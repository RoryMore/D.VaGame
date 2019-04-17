using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    //SHOOT RELATED VARIABLES
    int floorMask;
    //Laser variables

    //mechanical
    public float damagePerLaser = 20.0f;
    public float timeBetweenLasers = 0.12f;
    public float laserRange = 100f;

    public float laserDamageModifier = 0; // Decrease is bad
    public float laserTimeModifier = 0.0f; //Increase is bad
    public float laserRangeModifier = 0.0f;

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

        if (Physics.Raycast(shootRay, out shootHit, (laserRange - laserRangeModifier), shootableMask))

        {

            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

            if (enemyHealth != null)

            {

                enemyHealth.TakeDamage((damagePerLaser -laserDamageModifier), shootHit.point);

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

        if (Input.GetButton("Fire1") && timer >= (timeBetweenLasers + laserTimeModifier) && Time.timeScale != 0)

        {

            ShootLaser();

        }

        if (timer >= timeBetweenLasers + laserTimeModifier * laserDisplayTime)

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
