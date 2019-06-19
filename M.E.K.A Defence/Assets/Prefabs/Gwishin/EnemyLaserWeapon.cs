using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserWeapon : Weapon
{
    GameObject player;
    Gwishin movement;
    IEnumerator firing;
    IEnumerator charging;
    IEnumerator cool;
    bool dead = false;
    bool cooldownComplete = true;
    EnemyManager manager = null;
    Animator anim;

    public bool Dead { get => dead; set => dead = value; }
    private void Awake()
    {
        player = GameObject.Find("Player");
        movement = GetComponent<Gwishin>();
        manager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (dead) return;

        bool ammoIsFull = Stats.CurrentAmmo == Stats.AmmoCapacityBase;
        bool playerInRange = Vector3.Distance(player.transform.position, transform.position) < Stats.Range;
        bool thereArentTooManyOtherEnemiesFiring = manager.EnemiesFiring < manager.MaxEnemiesFiring;

        if (CanFire && ammoIsFull && playerInRange && !Firing && cooldownComplete && thereArentTooManyOtherEnemiesFiring)
        {
            manager.EnemiesFiring++;
            firing = FireLasers(Stats.FireRate, Stats.AmmoCapacity);
            StartCoroutine(firing);
        }
    }

    IEnumerator FireLasers(float frequency, float numberOfShots)
    {
        Firing = true;
        StartCoroutine(WeaponCharge(Stats.ChargeTime));
        movement.FiringLaser = true;
        anim.SetTrigger("StartAttack");

        while (Charging)
        {
            yield return new WaitForEndOfFrame();
        }


        for (int i = 0; i < numberOfShots; i++)
        {
            Vector3 playerPos = player.transform.position;
            playerPos.y += 10;
            Vector3 shotPosition = playerPos + Random.insideUnitSphere * Stats.Accurracy;

            EnemyLaserBall newlaser = Instantiate(Stats.Bullet, transform.position, transform.rotation).GetComponent<EnemyLaserBall>();
            newlaser.transform.LookAt(shotPosition);
            newlaser.Setup(Stats.BulletSpeed, Stats.BulletDamage);
            yield return new WaitForSeconds(frequency);
        }
        Firing = false;
        CanFire = false;
        movement.FiringLaser = false;

        cool = Cooldown();
        StartCoroutine(cool);
        manager.EnemiesFiring--;
        anim.SetTrigger("EndAttack");
    }

    IEnumerator Cooldown()
    {
        cooldownComplete = false;
        yield return new WaitForSeconds(Random.Range(5, 15));
        cooldownComplete = true;
    }


}
