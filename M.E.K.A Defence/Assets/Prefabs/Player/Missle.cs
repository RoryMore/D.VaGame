using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : MonoBehaviour
{
    //-----Movement
    //Velocity
    Vector3 velocity = Vector3.zero;
    public float maxVelocity = 15.0f;
    float maxVelocityModifierMax = 0.1f;
    //Acceleration
    Vector3 acceleration = Vector3.zero;
    public float accelerationRate = 1.0f;
    float accelerationRateModifierMax = 0.2f;
    //DesiredPositions
    public Vector3 targetPosition = Vector3.zero;
    public GameObject targetObject = null;

    //Rotation
    public float turnSpeed = 1.0f;
    float turnSpeedModifierMax = 0.5f;
    //Deviate
    Vector3 deviatePosition = Vector3.zero;
    float deviatePositionDistance = 2.0f;
    float deviatePointRadius = 1.0f;
    float deviateChance = 0.1f;
    //Height Restriction
    public float heightTarget = 1.0f;
    public float heightRangeMax = 0.25f;

    public float damage = 60.0f;


    private void Awake()
    {
        //targetPosition = transform.forward * 100.0f;
        Randomize();
    }
    private void FixedUpdate()
    {
        UpdateTarget();
        ProcessMovement(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage, other.transform.position);
            other.gameObject.GetComponent<GwishinMovement>().velocity += (other.transform.position - transform.position).normalized * damage * 2;
            if (other.gameObject.GetComponent<EnemyHealth>().currentHealth < 0)
            {
                other.gameObject.GetComponent<EnemyHealth>().killedByMissle = true;
            }

            //add explosiion before destroying
            Destroy(this.gameObject);
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
        transform.rotation = Quaternion.Slerp(transform.rotation, lookAtDirection, turnSpeed * Time.deltaTime);
    }

    //-----Deviate
    private void Deviate()
    {
        if (deviateChance > Random.value || deviatePosition == Vector3.zero || Vector3.Distance(targetPosition, transform.position) < Vector3.Distance(deviatePosition, targetPosition))
        {
            deviatePosition = transform.position + ((targetPosition - transform.position).normalized * deviatePositionDistance + Random.onUnitSphere * deviatePointRadius);
            deviatePosition.y = Mathf.Clamp(deviatePosition.y, heightTarget - heightRangeMax, heightTarget + heightRangeMax);
        }
    }

    private void Randomize()
    {
        maxVelocity *= 1 + Random.Range(-maxVelocityModifierMax, maxVelocityModifierMax);
        accelerationRate *= 1 + Random.Range(-accelerationRateModifierMax, accelerationRateModifierMax);
        turnSpeed *= 1 + Random.Range(-turnSpeedModifierMax, turnSpeedModifierMax);
    }

    void UpdateTarget()
    {
        

        if (targetObject != null)
        {
            targetPosition = targetObject.transform.position;
            heightTarget = targetObject.transform.position.y;

            if (targetObject.GetComponent<EnemyHealth>().isDead == true)
            {
                targetObject = null;
            }
        }
    }


}
