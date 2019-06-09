using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAiming : MonoBehaviour
{
    [SerializeField] Transform lowerHalf;
    public float turnSpeed;
    float angle;
    Quaternion targetRotation;

    //for raycast
    int floorMask;
    float camRayLength = 1000f;

    private void Awake()
    {
        floorMask = LayerMask.GetMask("Shootable");
    }
    private void FixedUpdate()
    {
        transform.position = lowerHalf.position;
        GetTargetRotation();
        Turn();
    }

    void GetTargetRotation()
    {
        //Casts a ray from camera location to mouse position in the scene
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        Vector3 playerToMouse;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {

            //Set the direction of the player towards the direction of the mouse

            playerToMouse = floorHit.point - transform.position;

            //Shouldn't be needed with the floor raycast but a double check to make

            //Sure the player cannot turn upwards

            

            
        }
        else playerToMouse = transform.position + camRay.direction.normalized * 1000;

        playerToMouse.y = 0f;
        targetRotation = Quaternion.LookRotation(playerToMouse);
    }

    void Turn()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed);
    }
}
