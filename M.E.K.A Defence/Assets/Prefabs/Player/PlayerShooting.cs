using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public float turnSpeed;

    float angle;
    Quaternion targetRotation;

    int floorMask;
    float camRayLength = 100f;


    //SHOOT RELATED VARIABLES
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.12f;
    public float range = 100f;

    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    int shootableMask;

    LineRenderer gunLine;

    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        shootableMask = LayerMask.GetMask("Shootable");

    }



    // Update is called once per frame
    void FixedUpdate()
    {
        Turning();
        IsLaserReady();
    }



    void Turning()
    {
        //Casts a ray from camera location to mouse position in the scene
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))

        {

            //Set the direction of the player towards the direction of the mouse

            Vector3 playerToMouse = floorHit.point - transform.position;

            //Shouldn't be needed with the floor raycast but a double check to make

            //Sure the player cannot turn upwards

            playerToMouse.y = 0f;

            targetRotation = Quaternion.LookRotation(playerToMouse);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed);
        }
    }


    void ShootLaser()
    {
        timer = 0f;
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))

        {

      
      
            gunLine.SetPosition(1, shootHit.point);

        }

        else

        {

            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);

        }
    }

    void IsLaserReady()
    {

        timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)

        {

            ShootLaser();

        }

    }
}
