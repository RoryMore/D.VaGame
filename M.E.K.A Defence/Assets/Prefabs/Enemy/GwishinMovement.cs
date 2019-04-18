using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EnemyState
{
    APPROACH,
    HALT,
    STRAFE,
    DEAD
}

public class GwishinMovement : MonoBehaviour
{
    //-----State
    EnemyState state = EnemyState.APPROACH;

    //-----Movement
    //Velocity
    public Vector3 velocity                     = Vector3.zero;
    public float maxVelocity             = 15.0f;
    float maxVelocityModifierMax         = 0.1f;
    //Acceleration
    Vector3 acceleration                 = Vector3.zero;
    public float accelerationRate        = 5.0f;
    float accelerationRateModifierMax    = 0.2f;
    //DesiredPositions
    Vector3 targetPosition               = Vector3.zero;

    //Rotation
    public float turnSpeed               = 1.0f;
    float turnSpeedModifierMax           = 0.5f;
    Vector3 rotationVector               = Vector3.zero;
    //Deviate
    Vector3 deviatePosition              = Vector3.zero;
    float deviatePositionDistance        = 5.0f;
    float deviatePointRadius             = 2.0f;
    float deviateChance                  = 0.1f;
    //Height Restriction
    public float heightTarget                  = 1.0f;
    public float heightRangeMax         = 0.50f;


    //-----References
    GameObject player;
    EnemyHealth myHealth;
    Animator anim;

    //Strafe area
    public Collider strafeArea = null;


    private void Awake()
    {
        player = GameObject.Find("Player");
        targetPosition = player.transform.position;
        heightTarget = targetPosition.y;
        myHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
        Randomize();
    }
    private void FixedUpdate()
    {
        if (transform.position.y < -20) Destroy(this);
        ProcessMovement();
        targetPosition = player.transform.position;
    }

//-----------------------------------------------------------------------//

    private void ProcessMovement()
    {
        UpdateState();

        switch (state)
        {
            case EnemyState.APPROACH:
                {
                    Deviate();
                    AccelerateFoward();
                    Rotate();
                }
                break;
            case EnemyState.HALT:
                {
                    Halt();
                    FaceTarget();
                }
                break;
            case EnemyState.STRAFE:
                {
                    GetStrafePosition();
                    Strafe();
                    FaceTarget();
                }
                break;
            case EnemyState.DEAD:
                {
                    Fall();
                    Spin();
                }
            break;
            default:
                {

                }
                break;
        }


        Move();        
    }
    private void AccelerateFoward()
    {
        acceleration = accelerationRate * transform.forward;
        velocity += acceleration;
        velocity = Vector3.ClampMagnitude(velocity, maxVelocity);
    }
    private void AccelerateToPoint()
    {
        acceleration = accelerationRate * (deviatePosition - transform.position).normalized;
        velocity += acceleration;
        velocity = Vector3.ClampMagnitude(velocity, maxVelocity/2.0f);
    }
    private void Move()
    {
        transform.position += velocity * Time.deltaTime;
    }
    private void Rotate()
    {
        Vector3 desiredDirection = deviatePosition - transform.position;
        //Vector3 desiredDirection = targetPosition;
        Quaternion lookAtDirection = Quaternion.LookRotation(desiredDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookAtDirection, turnSpeed * Time.deltaTime);
    }

    private void FaceTarget()
    {
        Vector3 desiredDirection = targetPosition - transform.position;
        //Vector3 desiredDirection = targetPosition;
        Quaternion lookAtDirection = Quaternion.LookRotation(desiredDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookAtDirection, turnSpeed * 10 * Time.deltaTime);
    }

    private void Deviate()
    {
        if (deviateChance > Random.value || deviatePosition == Vector3.zero || Vector3.Distance(targetPosition, transform.position) < Vector3.Distance(deviatePosition, targetPosition))
        { 
            deviatePosition = transform.position + ((targetPosition - transform.position).normalized * deviatePositionDistance + Random.onUnitSphere * deviatePointRadius);
            deviatePosition.y = Mathf.Clamp(deviatePosition.y, heightTarget - heightRangeMax, heightTarget + heightRangeMax);
        }
    }
    private void GetStrafePosition()
    {
        if (Vector3.Distance(deviatePosition, transform.position) < 1)
        {
            GetRandomStrafePosition();
        }
    }
    void GetRandomStrafePosition()
    {
        deviatePosition.x = Random.Range(strafeArea.bounds.min.x, strafeArea.bounds.max.x);
        deviatePosition.y = Random.Range(strafeArea.bounds.min.y, strafeArea.bounds.max.y);
        deviatePosition.z = Random.Range(strafeArea.bounds.min.z, strafeArea.bounds.max.z);
    }


    void Halt()
    {
        if (velocity.x != 0)
        {
            velocity.x -= accelerationRate * Mathf.Sign(velocity.x);
            if (Mathf.Abs(velocity.x) < accelerationRate)
            {
                velocity.x = 0;
            }
        }

        if (velocity.y != 0)
        {
            velocity.y -= accelerationRate * Mathf.Sign(velocity.y);
            if (Mathf.Abs(velocity.y) < accelerationRate)
            {
                velocity.y = 0;
            }
        }

        if (velocity.z != 0)
        {
            velocity.z -= accelerationRate * Mathf.Sign(velocity.z);
            if (Mathf.Abs(velocity.z) < accelerationRate)
            {
                velocity.z = 0;
            }
        }
            deviatePosition = player.transform.position;
    }

    private void Randomize()
    {
        maxVelocity         *= 1 + Random.Range(-maxVelocityModifierMax, maxVelocityModifierMax);
        accelerationRate    *= 1 + Random.Range(-accelerationRateModifierMax, accelerationRateModifierMax);
        turnSpeed           *= 1 + Random.Range(-turnSpeedModifierMax, turnSpeedModifierMax);
    }

    void UpdateState()
    {
        if (myHealth.isDead && state != EnemyState.DEAD)
        {
            state = EnemyState.DEAD;
            deviatePosition = transform.position + Random.onUnitSphere * 5;
            deviatePosition.y = -20;

            if (myHealth.killedByMissle)
            {
                velocity.x = Random.Range(-25, 25);
                velocity.y = Random.Range(25, 50);
                velocity.z = Random.Range(10, 25);
                rotationVector = Random.onUnitSphere * 250;
            }
            else
            {
                velocity.x = Random.Range(-10, 10);
                velocity.z = Random.Range(-10, 10);
                rotationVector = Random.onUnitSphere * 50;
            }
            

        }
        if (Vector3.Distance(transform.position, player.transform.position) < Vector3.Distance(strafeArea.bounds.center, player.transform.position) && state == EnemyState.APPROACH)
        {
            state = EnemyState.HALT;
            anim.SetBool("AttackMode", true);
        }
        else if (state == EnemyState.HALT && velocity == Vector3.zero)
        {
            state = EnemyState.STRAFE;
            GetRandomStrafePosition();
        }
    }

    void Strafe()
    {
        AccelerateToPoint();
    }

    void Fall()
    {
        acceleration = accelerationRate * (deviatePosition - transform.position).normalized;
        velocity.y += acceleration.y;
        
    }

    void Spin()
    {
        transform.Rotate(rotationVector * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.tag == "Enemy")
        {
            velocity += (transform.position - other.transform.position) * accelerationRate;
            
        }
    }
}
