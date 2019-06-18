using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserWeapon : Weapon
{
    GameObject player;
    Gwishin movement;
    IEnumerator firing;
    IEnumerator charging;

    private void Awake()
    {
        player = GameObject.Find("Player");
        movement = GetComponent<Gwishin>();
    }
    private void Update()
    {
        bool ammoIsFull = Stats.CurrentAmmo == Stats.AmmoCapacityBase;
        bool playerInRange = Vector3.Distance(player.transform.position, transform.position) < Stats.Range;

        if (CanFire && ammoIsFull && playerInRange && !Firing)
        {
            firing = FireLasers(Stats.FireRate);
            StartCoroutine(firing);
        }
    }

    IEnumerator FireLasers(float frequency)
    {
        Firing = true;
        print(frequency);
        StartCoroutine(WeaponCharge(Stats.ChargeTime));
        movement.FiringLaser = true;
        while (Charging)
        {
            yield return new WaitForEndOfFrame();
        }


        for (int i = 0; i <= Stats.CurrentAmmo; i++)
        {
            Vector3 shotPosition = player.transform.position + Random.onUnitSphere * Stats.Accurracy;

            EnemyLaserBall newlaser = Instantiate(Stats.Bullet, transform.position, transform.rotation).GetComponent<EnemyLaserBall>();
            newlaser.transform.LookAt(shotPosition);
            newlaser.Setup(Stats.BulletSpeed, Stats.BulletDamage);
            ReduceCurrentAmmo(1);
            print("FIRDE");
            yield return new WaitForSeconds(frequency);
        }
        Firing = false;
        CanFire = false;
        movement.FiringLaser = false;
    }

}
