using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWeapon : Weapon
{
    private void Update()
    {
        if (PlayerActivatesInput())
        {
            FireLaser();
        }
        else if (PlayerDeactivatesInput())
        {

        }
    }

    void FireLaser()
    {
        Instantiate(Stats.Bullet, transform.position, transform.rotation);
    }

    void Awake()
    {
        this.Stats = PlayerModifierManager.Instance.LaserWeaponStats ;
    }
}
