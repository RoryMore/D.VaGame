﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWeapon : Weapon
{
    IEnumerator startFiring;
    Vector3 targetPosition;

    private void Awake()
    {
        startFiring = StartFiring(Stats.FireRate, Stats.CurrentAmmo);
    }

    private void Update()
    {
        if (PlayerActivatesInput() && !Firing)
        {
            StopCoroutine(startFiring);
            startFiring = StartFiring(Stats.FireRate, Stats.CurrentAmmo);
            StartCoroutine(startFiring);
        }

        UpdateTarget();
    }

    IEnumerator StartFiring(float frequency, int numberOfShots)
    {
        Firing = true;
        for (int i = 0; i < numberOfShots; i++)
        {
            GunBullet newBullet = Instantiate(Stats.Bullet, transform.position, transform.rotation).GetComponent<GunBullet>();
            newBullet.transform.LookAt(targetPosition + Random.onUnitSphere * Stats.Accurracy);
            newBullet.SetupBullet(Stats.BulletSpeed, Stats.BulletDamage);
            ReduceCurrentAmmo(1);
            yield return new WaitForSeconds(frequency);
        }
        Firing = false;
    }

    void UpdateTarget()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(mouseRay, out hitInfo, Stats.Range, LayerMask.GetMask("Shootable")))
        {
            targetPosition = hitInfo.point;
        }
        else
        {
            targetPosition = transform.position + mouseRay.direction.normalized * Stats.Range;
        }
    }
}
