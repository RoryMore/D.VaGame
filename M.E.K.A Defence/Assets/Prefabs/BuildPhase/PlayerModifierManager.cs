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
    float moveSpeedModifier = 0.0f;
    float turnSpeedModifier = 0.0f;

    //Laser Modifiers
    float laserTimeModifier = 0.0f;
    float laserRangeModifier = 0.0f;
    float laserDamageModifier = 0;

    //Missle Modifiers
    float missileTimeModifier = 0.0f;
    float missileAmmoModifier = 0.0f;
    float missileDamageModifer = 0.0f;

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
        moveSpeedModifier = 0.0f;
        turnSpeedModifier = 0.0f;
        laserTimeModifier = 0.0f;
        laserRangeModifier = 0.0f;
        laserDamageModifier = 0;
        missileTimeModifier = 0.0f;
        missileAmmoModifier = 0.0f;
        missileDamageModifer = 0.0f;


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

    

}
