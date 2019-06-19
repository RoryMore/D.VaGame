using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : MonoBehaviour
{
    //-----Movement
    //Velocity
    Vector3 velocity = Vector3.zero;
    float maxVelocity = 30.0f;
    float maxVelocityModifierMax = 0.0f;
    //Acceleration
    Vector3 acceleration = Vector3.zero;
    float accelerationRate = 10f;
    float accelerationRateModifierMax = 0.0f;
    //DesiredPositions
    Vector3 targetPosition = Vector3.zero;
    public GameObject targetObject = null;

    //Rotation
    float turnSpeedModifierMax = 0.0f;
    //Deviate
    Vector3 deviatePosition = Vector3.zero;
    float deviatePositionDistance = 5.0f;
    float deviatePointRadius = 3f;
    float deviateChance = 0.5f;
    //Height Restriction
    float heightTarget = 1.0f;
    float heightRangeMax = 10f;

    float damage = 60.0f;
    float damageRadius = 5f;

    // ----- Sound ----- //
    AudioSource source;
    [SerializeField] AudioClip[] launchClips;
    [SerializeField] AudioClip[] explodeClips;

    // ----- Effects ----- //
    [SerializeField] GameObject explosionEffect;
    [SerializeField] GameObject steamEffect;
    TrailRenderer trail;


    bool dead = false;
    
    public float MaxVelocity { get => maxVelocity; set => maxVelocity = value; }
    public float Damage { get => damage; set => damage = value; }
    public float DamageRadius { get => damageRadius; set => damageRadius = value; }

    private void Awake()
    {
        //targetPosition = transform.forward * 100.0f;
        Randomize();
        source = GetComponent<AudioSource>();
        trail = GetComponent<TrailRenderer>();

        //play sound
        int n = Random.Range(0, launchClips.Length);
        source.clip = launchClips[n];
        source.Play();

        // Create Steam
        Instantiate(steamEffect, transform.position, transform.rotation);

        StartCoroutine(Death());
    }
    private void FixedUpdate()
    {
        if (dead)
        {
            if (!source.isPlaying) Destroy(this.gameObject);
        }
        else
        {
            UpdateTarget();
            ProcessMovement();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (dead) return;
        if (other.gameObject.tag == "Enemy")
        {
            //Deal Damage
            if (other.gameObject.GetComponent<EnemyHealth>().currentHealth - damage <= 0)
            {
                other.gameObject.GetComponent<EnemyHealth>().killedByMissle = true;
            }
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage, other.transform.position);
            other.gameObject.GetComponent<Gwishin>().RemoveTargeted();



            //Create Explosion
            GameObject explosion = Instantiate(explosionEffect, transform.position, Random.rotation);

            //PlaySound
            int n = Random.Range(0, explodeClips.Length);
            source.clip = explodeClips[n];
            source.Play();

            dead = true;
            Destroy(GetComponent<MeshRenderer>());
            Destroy(GetComponent<SphereCollider>());
        }
    }

    //-----------------------------------------------------------------------//

    private void ProcessMovement()
    {
        Deviate();
        Rotate();
        Accelerate();
        Move();
    }
    private void Accelerate()
    {
        acceleration = accelerationRate * transform.forward;
        velocity += acceleration;
        velocity = Vector3.ClampMagnitude(velocity, maxVelocity);
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
        transform.rotation = Quaternion.Slerp(transform.rotation, lookAtDirection, 0.1f);
    }

    //-----Deviate
    private void Deviate()
    {
        if (deviateChance > Random.value  || Vector3.Distance(targetPosition, transform.position) < Vector3.Distance(deviatePosition, targetPosition))
        {
            deviatePosition = transform.position + ((targetPosition - transform.position).normalized * deviatePositionDistance + Random.onUnitSphere * deviatePointRadius);
            deviatePosition.y = Mathf.Clamp(deviatePosition.y, heightTarget - heightRangeMax, heightTarget + heightRangeMax);
        }
    }

    private void Randomize()
    {
        maxVelocity *= 1 + Random.Range(-maxVelocityModifierMax, maxVelocityModifierMax);
        accelerationRate *= 1 + Random.Range(-accelerationRateModifierMax, accelerationRateModifierMax);
    }

    void UpdateTarget()
    {
        if (targetObject != null)
        {
            targetPosition = targetObject.transform.position;
            heightTarget = targetObject.transform.position.y;

            if (targetObject.GetComponent<EnemyHealth>().dead == true)
            {
                targetObject = null;
            }
        }
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(30);
        Destroy(this.gameObject);
    }
}
