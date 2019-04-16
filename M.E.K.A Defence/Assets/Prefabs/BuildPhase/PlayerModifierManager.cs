using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerModifierManager : Singleton<PlayerModifierManager>

{

    //Player Modifiers
    float moveSpeedModifier = 0.0f;
    float turnSpeedModifier = 0.0f;
    float laserTimeModifier = 0.0f;
    float laserRangeModifier = 0.0f;
    float laserDamageModifier = 0;

    //Menu Management Variables
    public void Start()
    {

    }
    //FUNCTIONS
    //void 

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
