using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GwishinMovement : MonoBehaviour
{
    //-----Movement
    //Velocity
    Vector3 velocity                     = Vector3.zero;
    public float maxVelocity             = 15.0f;
    float maxVelocityModifierMax         = 0.1f;
    //Acceleration
    Vector3 acceleration                 = Vector3.zero;
    public float accelerationRate        = 1.0f;
    float accelerationRateModifierMax    = 0.2f;
    //DesiredPositions
    Vector3 targetPosition               = Vector3.zero;

    //Rotation
    public float turnSpeed               = 1.0f;
    float turnSpeedModifierMax           = 0.5f;
    //Deviate
    Vector3 deviatePosition              = Vector3.zero;
    float deviatePositionDistance        = 2.0f;
    float deviatePointRadius             = 5.0f;
    float deviateChance                  = 0.1f;
    //Height Restriction
    public float heightTarget           = 0.0f;
    public float heightRangeMax         = 1.0f;


    //-----References
    GameObject player;


    private void Awake()
    {
        player = GameObject.Find("Player");
        targetPosition = player.transform.position;
        Randomize();
    }
    private void FixedUpdate()
    {
        ProcessMovement();
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
            deviatePosition.y = Mathf.Clamp(deviatePosition.y, -heightRangeMax, heightRangeMax);
        }
    }

    private void Randomize()
    {
        maxVelocity         *= 1 - Random.Range(-maxVelocityModifierMax, maxVelocityModifierMax);
        accelerationRate    *= 1 - Random.Range(-accelerationRateModifierMax, accelerationRateModifierMax);
        turnSpeed           *= 1 - Random.Range(-turnSpeedModifierMax, turnSpeedModifierMax);
    }
}
