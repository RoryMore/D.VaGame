using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    //Upper body Rotation
    public float turnSpeed;

    float angle;
    Quaternion targetRotation;

    int floorMask;
    float camRayLength = 100f;


    //SHOOT RELATED VARIABLES

    //Laser variables

    //mechanical
    public int damagePerLaser = 20;
    public float timeBetweenLasers = 0.12f;
    public float laserRange = 100f;

    public int laserDamageModifer = 0; // Decrease is bad
    public int laserTimeModifer = 0; //Increase is bad
    public int laserRangeModifier = 0;

    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    int shootableMask;

    //Cosmetic
    Light laserLight;
    float laserDisplayTime = 0.2f;
    LineRenderer laserLine;

    Color laserColor1 = Color.cyan;
    Color laserColor2 = new Color(0, 1, 1, 0);

    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        shootableMask = LayerMask.GetMask("Shootable");

        laserLight = GetComponent<Light>();
        laserLine = GetComponent<LineRenderer>();

    }



    // Update is called once per frame
    void FixedUpdate()
    {
        //Update Timer
        timer += Time.deltaTime;

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

        //LASER BEAM SOUND

        laserLight.enabled = true;

        laserLine.enabled = true;
        laserLine.SetPosition(0, transform.position);
        

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if (Physics.Raycast(shootRay, out shootHit, (laserRange - laserRangeModifier), shootableMask))

        {

            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

            if (enemyHealth != null)

            {

                enemyHealth.TakeDamage((damagePerLaser - laserDamageModifer), shootHit.point);

            }

            laserLine.SetPosition(1, shootHit.point);

        }

        else

        {

            laserLine.SetPosition(1, shootRay.origin + shootRay.direction * (laserRange - laserRangeModifier));

        }
    }

    void IsLaserReady()
    {

        if (Input.GetButton("Fire1") && timer >= (timeBetweenLasers + laserTimeModifer) && Time.timeScale != 0)

        {

            ShootLaser();

        }

        if (timer >= timeBetweenLasers + laserTimeModifer * laserDisplayTime)

        {

            DisableEffects();

        }

    }

    void DisableEffects()
    {
        laserLine.enabled = false;
        laserLight.enabled = false;
    }
}
