﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerModifierManager : Singleton<PlayerModifierManager>

{
    WeaponStats missleWeaponStats = null;
    WeaponStats laserWeaponStats = null;
    WeaponStats gunWeaponStats = null;


    public WeaponStats MissleWeaponStats { get => missleWeaponStats; set => missleWeaponStats = value; }
    public WeaponStats LaserWeaponStats { get => laserWeaponStats; set => laserWeaponStats = value; }
    public WeaponStats GunWeaponStats { get => gunWeaponStats; set => gunWeaponStats = value; }

    
    


    //Global Values
    float waveCount = 0;

    //You are the worst mech. **NO-ONE** cares about your station any more than they need to. You start the game worse for wear
    //And suffer initial damage to multiple systems

    //Move Modifiers
    float moveSpeedModifier = 0.8f;
    float turnSpeedModifier = 0.8f;
    //Visual Modifier for slider
    float movementHealth = 0.8f;

    //Laser Modifiers
    float laserTimeModifier = 0.7f;
    float laserRangeModifier = 0.7f;
    float laserDamageModifier = 0.7f;
    //Visual Modifier for slider
    float laserGunHealth = 0.7f;

    //Missle Modifiers
    public float missileTimeModifier = 0.3f;
    public float missileAmmoModifier = 0.3f;
    public float missileDamageModifer = 0.3f;
    //Visual Modifier for slider
    float missileHealth = 0.3f;

    public float machineTimeModifier = 0.5f;
    public float machineAmmoModifier = 0.5f;
    public float machineDamageModifer = 0.5f;

    float machineHealth = 0.5f;

    //Upgrade Variables. Bools and ints to determine if you have certain upgrades and what level, if applicable
    bool hasDash = false;
    bool hasLaser = false;
    bool hasMissile = false;
    bool hasMachine = false;


    public void Start()
    {

    }

    //FUNCTIONS
    public void ResetModifiers()
    {
        //Global Values
        waveCount = 0;

        //Player Modifiers
        //Movement
        moveSpeedModifier = 0.8f;
        turnSpeedModifier = 0.8f;
   
        movementHealth = 0.8f;

        //Laser
        laserTimeModifier = 0.7f;
        laserRangeModifier = 0.7f;
        laserDamageModifier = 0.7f;

        laserGunHealth = 0.7f;
        // Missile
        missileTimeModifier = 0.3f;
        missileAmmoModifier = 0.3f;
        missileDamageModifer = 0.3f;

        missileHealth = 0.3f;

        machineTimeModifier = 0.5f;
        machineAmmoModifier = 0.5f;
        machineDamageModifer = 0.5f;

        machineHealth = 0.5f;

        hasDash = false;
        hasMissile = false;
        hasLaser = false;
        hasMachine = false;

    }

    public void UpdateWeaponStats()
    {

        //Laser
        LaserWeaponStats.ReplenishRate = LaserWeaponStats.ReplenishRateBase * laserTimeModifier;
        LaserWeaponStats.Range = 30 * laserRangeModifier;
        LaserWeaponStats.BulletDamage = 10 * laserDamageModifier;

        ////Machine Gun (we really need to do this....)
        //GunWeaponStats.ReplenishRate = GunWeaponStats.ReplenishRateBase * machineTimeModifier;
        //GunWeaponStats.AmmoCapacity = (int)(GunWeaponStats.AmmoCapacityBase * machineTimeModifier);
        //GunWeaponStats.BulletDamage = GunWeaponStats.BulletDamageBase * machineDamageModifer;


        // Missile
        MissleWeaponStats.AmmoCapacity = (int)(MissleWeaponStats.AmmoCapacityBase * missileAmmoModifier);
        MissleWeaponStats.ReplenishRate = MissleWeaponStats.ReplenishRateBase * missileTimeModifier;
        MissleWeaponStats.BulletDamage = MissleWeaponStats.BulletDamageBase * missileDamageModifer;




    }

    //SETTER
    public void CalculateModifier(string modifier, float value)
    {
        if (modifier == "moveSpeed")
        {
            moveSpeedModifier += value;
        }
        else if (modifier == "turnSpeed")
        {
            turnSpeedModifier += value;
        }
        else if (modifier == "laserTime")
        {
            laserTimeModifier += value;
        }
        else if (modifier == "laserRange")
        {
            laserRangeModifier += value;
        }
        else if (modifier == "laserDamage")
        {
            laserDamageModifier += value;
        }
        else if (modifier == "missileTime")
        {
            missileTimeModifier += value;
        }
        else if (modifier == "missileAmmo")
        {
            missileAmmoModifier += value;
        }
        else if (modifier == "missileDamage")
        {
            missileDamageModifer += value;
        }
        else if (modifier == "machineTime")
        {
            machineTimeModifier += value;
        }
        else if (modifier == "machineAmmo")
        {
            machineAmmoModifier += value;
        }
        else if (modifier == "machineDamage")
        {
            machineDamageModifer += value;
        }
        //Visual values. Used Exclusivley for the sliders
        else if (modifier == "movementHealth")
        {
            movementHealth += value;
        }
        else if (modifier == "laserGunHealth")
        {
            laserGunHealth += value;
        }
        else if (modifier == "missileHealth")
        {
            missileHealth += value;
        }
        else if (modifier == "machineHealth")
        {
            machineHealth += value;
        }

    }

    public void UpgradeDash()
    {
        hasDash = true;
    }

    public void UpgradeLaser()
    {
        hasLaser = true;
    }

    public void UpgradeMissile()
    {
        hasMissile = true;
    }

    public void UpgradeMachine()
    {
        hasMachine = true;
    }


    //GETTERS

    public float GetWaveCount()
    {
        return waveCount;
    }

    public float GetMoveSpeed()
    {
        return moveSpeedModifier;
    }

    public float GetTurnSpeed()
    {
        return turnSpeedModifier;
    }

    public float GetLaserTime()
    {
        return laserTimeModifier;
    }

    public float GetLaserRange()
    {
        return laserRangeModifier;
    }

    public float GetLaserDamage()
    {
        return laserDamageModifier;
    }

    public float GetMissileDamage()
    {
        return missileDamageModifer;
    }
    
    public float GetMissileAmmoCapacity()
    {
        return missileAmmoModifier;
    }

    public float GetMissileReplenishRate()
    {
        return missileTimeModifier;
    }

    public float GetMachineDamage()
    {
        return machineDamageModifer;
    }

    public float GetMachineAmmoCapacity()
    {
        return machineAmmoModifier;
    }

    public float GetMachineReplenishRate()
    {
        return machineTimeModifier;
    }

    public float GetMovementHealth()
    {
        return movementHealth;
    }

    public float GetLaserGunHealth()
    {
        return laserGunHealth;
    }

    public float GetMachineHealth()
    {
        return machineHealth;
    }

    public float GetMissileHealth()
    {
        return missileHealth;
    }

    public bool GetHasDash()
    {
        return hasDash;
    }

    public bool GetHasLaser()
    {
        return hasLaser;
    }

    public bool GetHasMissile()
    {
        return hasMissile;
    }

    public bool GetHasMachine()
    {
        return hasMachine;
    }


}
