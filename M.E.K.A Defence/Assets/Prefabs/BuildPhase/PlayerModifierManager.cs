using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModifierManager : Singleton<PlayerModifierManager>

{
    float moveSpeedModifier = 0.0f;
    float turnSpeedModifier = 0.0f;
    float laserTimeModifier = 0.0f;
    float laserRangeModifier = 0.0f;
    float laserDamageModifier = 0;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

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
