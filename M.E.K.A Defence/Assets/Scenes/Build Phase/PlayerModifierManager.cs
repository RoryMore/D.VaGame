using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerModifierManager : Singleton<PlayerModifierManager>

{
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

    //Menu Management Variables
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

    public float GetMovementHealth()
    {
        return movementHealth;
    }

    public float GetLaserGunHealth()
    {
        return laserGunHealth;
    }

    public float GetMissileHealth()
    {
        return missileHealth;
    }

}
