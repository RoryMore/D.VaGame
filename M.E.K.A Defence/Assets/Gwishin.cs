using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EnemyState
{
    WARP,
    APPROACH,
    HALT,
    STRAFE,
    DEAD
}
struct Modifiers
{
    float velocity;
    float acceleration;
    float turnspeed;

    public float Velocity { get => velocity; set => velocity = value; }
    public float Acceleration { get => acceleration; set => acceleration = value; }
    public float Turnspeed { get => turnspeed; set => turnspeed = value; }

    public void Randomize()
    {
        float velocityMax = 0.1f;
        float accelerationMax = 0.2f;
        float turnSpeedMax = 0.5f;

        velocity = 1 + Random.Range(-velocityMax, velocityMax);
        acceleration = 1 + Random.Range(-accelerationMax, accelerationMax);
        turnspeed = 1 + Random.Range(-turnSpeedMax, turnSpeedMax);
    }

}
struct MovementStats
{
    float accelerationRate;
    float maxVelocity;
    float turnSpeed;

    public float TurnSpeed { get => turnSpeed; set => turnSpeed = value; }
    public float MaxVelocity { get => maxVelocity; set => maxVelocity = value; }
    public float AccelerationRate { get => accelerationRate; set => accelerationRate = value; }

    public void Setup(float accelerationRate, float maxVelocity, float turnSpeed)
    {
        this.accelerationRate = accelerationRate;
        this.maxVelocity = maxVelocity;
        this.turnSpeed = turnSpeed;
    }
}

public class Gwishin : MonoBehaviour
{
    Vector3 velocity;
    Vector3 acceleration;
    Vector3 rotationVector;
    MovementStats approachStats;
    MovementStats warpStats;
    MovementStats currentStats;
    EnemyState currentState = EnemyState.APPROACH;
    Modifiers modifier;
    Vector3 targetPosition = Vector3.zero;
    GameObject player = null;
    EnemyHealth health = null;
    bool firingLaser = false;

    List<TrailRenderer> trails = new List<TrailRenderer>();

    [SerializeField] GameObject targetedReticle = null;
    List<GameObject> reticles = new List<GameObject>();
    int timesTargeted = 0;
    float distanceFromPlayer = 0;

    //distances
    float strafeDistance = 200.0f;
    float approachDistance = 500.0f;

    public bool FiringLaser { get => firingLaser; set => firingLaser = value; }
    public int TimesTargeted { get => timesTargeted; set => timesTargeted = value; }
    public float DistanceFromPlayer { get => distanceFromPlayer; set => distanceFromPlayer = value; }
    public Vector3 Velocity { get => velocity; set => velocity = value; }

    private void Awake()
    {
        approachStats.Setup(2.5f, 25f, 30f);
        warpStats.Setup(1000f, 2000f, 0);
        modifier.Randomize();
        currentStats = approachStats;
        transform.rotation = Random.rotation;
        trails.Add(transform.Find("Left Trail").GetComponent<TrailRenderer>());
        trails.Add(transform.Find("Right Trail").GetComponent<TrailRenderer>());
        player = GameObject.Find("Player");
        health = GetComponent<EnemyHealth>();
    }
    private void Update()
    {
        distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (firingLaser && currentState != EnemyState.DEAD) return;
        UpdateState();
        ProcessState();


    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            this.transform.position += (transform.position - other.transform.position).normalized * Time.deltaTime * 4;
        }
    }
    private void Move()
    {
        transform.position += velocity * Time.deltaTime;
    }
    private void Rotate(Vector3 target)
    {
        Vector3 desiredDirection = target - transform.position;
        Quaternion lookAtDirection = Quaternion.LookRotation(desiredDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookAtDirection, currentStats.TurnSpeed * modifier.Turnspeed * Time.deltaTime);
    }
    private void Accelerate(Vector3 direction)
    {
        acceleration = currentStats.AccelerationRate * modifier.Acceleration * direction;
        velocity += acceleration;
        velocity = Vector3.ClampMagnitude(velocity, currentStats.MaxVelocity * modifier.Velocity);
        if ((transform.position + velocity * Time.deltaTime).y <= 0) velocity.y = currentStats.MaxVelocity;
    }
    private void Halt()
    {
        velocity = Vector3.Lerp(velocity, Vector3.zero, 0.5f);
    }
    private void UpdateState()
    {
        switch (currentState)
        {
            case EnemyState.APPROACH:
                {
                    bool outsideApproachRange = Vector3.Distance(transform.position, targetPosition) > approachDistance;
                    bool lookingAtTarget = Quaternion.Angle(transform.rotation, Quaternion.LookRotation(targetPosition - transform.position)) < 7f;
                    bool notAimingAtGround = (transform.position + transform.forward * (Vector3.Distance(transform.position, targetPosition) - approachDistance)).y > targetPosition.y - 10;
                    bool notAimingToHigh = (transform.position + transform.forward * (Vector3.Distance(transform.position, targetPosition) - approachDistance)).y < targetPosition.y + 150;
                    bool randomness = Random.value < 0.2f;
                    //bool lookingAtTarget = transform.rotation == Quaternion.LookRotation(targetPosition.position - transform.position);

                    if (outsideApproachRange && lookingAtTarget && notAimingAtGround && notAimingToHigh && randomness)
                    {
                        currentState = EnemyState.WARP;
                        currentStats = warpStats;
                        foreach (TrailRenderer trail in trails)
                        {
                            trail.emitting = true;
                        }
                        break;
                    }

                    bool insideStrafeRange = Vector3.Distance(transform.position, targetPosition) < strafeDistance;

                    if (insideStrafeRange)
                    {
                        currentState = EnemyState.STRAFE;
                        SetRandomStrafePosition();
                    }
                }
                break;
            case EnemyState.WARP:
                {
                    bool withinApproachRange = Vector3.Distance(transform.position, targetPosition) < approachDistance;

                    if (withinApproachRange)
                    {
                        currentState = EnemyState.HALT;
                        foreach (TrailRenderer trail in trails)
                        {
                            trail.emitting = false;
                        }
                    }
                }
                break;
            case EnemyState.HALT:
                {

                    if (velocity.magnitude < 1)
                    {
                        bool insideStrafeRange = Vector3.Distance(transform.position, targetPosition) < strafeDistance;

                        if (insideStrafeRange)
                        {
                            currentState = EnemyState.STRAFE;
                            SetRandomStrafePosition();
                        }
                        else
                        {
                            currentState = EnemyState.APPROACH;
                            currentStats = approachStats;
                        }
                    }
                }
                break;
            default:
                break;
        }
    }
    private void ProcessState()
    {
        switch (currentState)
        {
            case EnemyState.APPROACH:
                {
                    targetPosition = player.transform.position;
                    Rotate(targetPosition);
                    Accelerate(transform.forward);
                    Move();
                }
                break;
            case EnemyState.WARP:
                {
                    Accelerate(transform.forward);
                    Move();
                }
                break;
            case EnemyState.HALT:
                {
                    Rotate(targetPosition);
                    Halt();
                }
                break;
            case EnemyState.STRAFE:
                {
                    Rotate(player.transform.position);
                    Accelerate((targetPosition - transform.position).normalized);
                    
                    if (Vector3.Distance(targetPosition, transform.position) < 1)
                    {
                        SetRandomStrafePosition();
                    }

                    Move();
                }
                break;
            case EnemyState.DEAD:
                {
                    Fall();
                    Spin();
                    Move();

                    if (transform.position.y < -20) Destroy(this.gameObject);
                }
                break;
            default:
                break;
        }
    }
    private void SetRandomStrafePosition()
    {
        targetPosition.x = Random.Range(player.transform.position.x - strafeDistance * 1.5f, player.transform.position.x + strafeDistance * 1.5f);
        targetPosition.y = Random.Range(player.transform.position.y + 25, player.transform.position.y + 50);
        targetPosition.z = Random.Range(player.transform.position.z + strafeDistance * 0.5f, player.transform.position.z + strafeDistance * 2);
    }
    public void Die()
    {
        currentState = EnemyState.DEAD;

        targetPosition = transform.position + Random.onUnitSphere * 10;
        targetPosition.y = -20;

        if (true)
        {//health.killedByMissle
            velocity.x = Random.Range(-50, 50);
            velocity.y = Random.Range(25, 50);
            velocity.z = Random.Range(10, 50);
            rotationVector = Random.onUnitSphere * 250;
        }
        else
        {
            velocity.x += Random.Range(-10, 10);
            velocity.z += Random.Range(-10, 10);
            rotationVector = Random.onUnitSphere * 50;
        }
    }
    private void Fall()
    {
        acceleration = currentStats.AccelerationRate * (targetPosition - transform.position).normalized;
        velocity.y += acceleration.y;
    }
    private void Spin()
    {
        transform.Rotate(rotationVector * Time.deltaTime);
    }

    public void AddTargetedReticle()
    {
        GameObject reticle = Instantiate(targetedReticle, transform.position, transform.rotation);
        reticle.transform.localScale *= 2;
        reticle.GetComponent<EnemyTargetedReticle>().Following = this.gameObject;
        //reticle.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        reticles.Add(reticle);

    }
    public void RemoveTargeted()
    {
        if (reticles.Count > 0)
        {
            GameObject toDestroy = reticles[reticles.Count - 1];
            reticles.RemoveAt(reticles.Count - 1);
            Destroy(toDestroy.gameObject);
        }
        timesTargeted--;
    }
}
