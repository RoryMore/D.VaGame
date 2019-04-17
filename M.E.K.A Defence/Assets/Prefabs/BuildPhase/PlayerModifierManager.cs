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

    //Laser Modifiers
    float laserTimeModifier = 1.0f;
    float laserRangeModifier = 1.0f;
    float laserDamageModifier = 1.0f;

    //Missle Modifiers
    float missileTimeModifier = 1.0f;
    float missileAmmoModifier = 1.0f;
    float missileDamageModifer = 1.0f;

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

        laserTimeModifier = 1.0f;
        laserRangeModifier = 1.0f;
        laserDamageModifier = 1.0f;

        missileTimeModifier = 1.0f;
        missileAmmoModifier = 1.0f;
        missileDamageModifer = 1.0f;


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

}
