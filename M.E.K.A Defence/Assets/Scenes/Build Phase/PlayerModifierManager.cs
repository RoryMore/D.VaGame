using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerModifierManager : Singleton<PlayerModifierManager>

{
    //Global Values
    float waveCount = 0;

    //Move Modifiers
    float moveSpeedModifier = 1.0f;
    float turnSpeedModifier = 1.0f;
    //Visual Modifier for slider
    float movementHealth = 0.5f;

    //Laser Modifiers
    float laserTimeModifier = 1.0f;
    float laserRangeModifier = 1.0f;
    float laserDamageModifier = 1.0f;
    //Visual Modifier for slider
    float laserGunHealth = 0.5f;

    //Missle Modifiers
    public float missileTimeModifier = 1.0f;
    public float missileAmmoModifier = 1.0f;
    public float missileDamageModifer = 1.0f;
    //Visual Modifier for slider
    float missileHealth = 0.5f;

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
        moveSpeedModifier = 1.0f;
        turnSpeedModifier = 1.0f;

        movementHealth = 1.0f;

        laserTimeModifier = 1.0f;
        laserRangeModifier = 1.0f;
        laserDamageModifier = 1.0f;

        laserGunHealth = 1.0f;

        missileTimeModifier = 1.0f;
        missileAmmoModifier = 1.0f;
        missileDamageModifer = 1.0f;

        missileHealth = 1.0f;

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
