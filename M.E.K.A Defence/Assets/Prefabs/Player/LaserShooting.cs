using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShooting : MonoBehaviour
{
    //SHOOT RELATED VARIABLES
   
    //Laser variables

    //-----mechanical
    public int damagePerLaser = 20;
    public float timeBetweenLasers = 0.12f;
    public float laserRange = 100f;
    float timer;
    //for raycast
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    int shootableMask;
    int floorMask;

    //-----Cosmetic
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
        IsLaserReady();
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
        //add mosue picking so we can aim up

        if (Physics.Raycast(shootRay, out shootHit, laserRange, shootableMask))

        {

            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

            if (enemyHealth != null)

            {

                enemyHealth.TakeDamage(damagePerLaser, shootHit.point);

            }

            laserLine.SetPosition(1, shootHit.point);

        }

        else

        {

            laserLine.SetPosition(1, shootRay.origin + shootRay.direction * laserRange);

        }
    }

    void IsLaserReady()
    {

        if (Input.GetButton("Fire1") && timer >= timeBetweenLasers && Time.timeScale != 0)

        {

            ShootLaser();

        }

        if (timer >= timeBetweenLasers * laserDisplayTime)

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
