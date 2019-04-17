using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BuildPhaseManager : MonoBehaviour
{
    Slider laserDamageSlider;
    Slider laserFireRateSlider;
    Slider laserRangeSlider;

    Slider missileDamageSlider;
    //CHANGE THIS IN UI FROM "Fire Rate" to "Ammo Capacity"
    Slider missileAmmoCapacitySlider;
    //CHANGE THIS IN UI from "Range" to "Replenish Rate"
    Slider missileReplenishRate;

    Slider moveSpeedSlider;
    Slider turnSpeedSlider;

    //Time Counting Variables
    public Text timeLeftText;
    private float timeLeft = 4;

    //Min and max are 0 and 2 respectivley, start at 1
    //Make sure increments in 0.1, multiply instead of add like you do right now you absolute nonce
    //Set the slider value to be equal to its singleton value correspondant every frame (or just get it to do it when launch goes and pass all values then EXTENSION) 
    //Set value to be multiplier

    //Plus button for each slider
    //If plus button clicked, -1 week from time resource and + 0.1 to value
    //Only allow button to run if week != 0

    //Copy "MachineGunFireRangeSlider" for the correct set up of the slider



    private void Awake()
    {
        //Laser Sliders
        laserDamageSlider = GameObject.Find("LaserGunFireDamageSlider").GetComponent<Slider>();
        laserFireRateSlider = GameObject.Find("LaserGunFireRateSlider").GetComponent<Slider>();
        laserRangeSlider = GameObject.Find("LaserGunFireRangeSlider").GetComponent<Slider>();

        //Missile Sliders
        missileDamageSlider = GameObject.Find("MissilePodFireDamageSlider").GetComponent<Slider>();
        missileAmmoCapacitySlider = GameObject.Find("MissilePodFireRangeSlider").GetComponent<Slider>();
        missileReplenishRate = GameObject.Find("MissilePodFireRateSlider").GetComponent<Slider>(); 

         //Move Sliders
         moveSpeedSlider = GameObject.Find("WalkSpeedSlider").GetComponent<Slider>();
        turnSpeedSlider = GameObject.Find("TurnSpeedSlider").GetComponent<Slider>();

        timeLeftText = GameObject.Find("TimeLeftText").GetComponent<Text>();

        UpdateAllVariables();
    }
    public void StartWave()
    {
        //CHANGE THIS TO GAME SCENE
        SceneManager.LoadScene("EnemyTest");
        //SceneManager.LoadScene("ShmupScene");
    }

    private void UpdateAllVariables()
    {
        laserDamageSlider.value = PlayerModifierManager.Instance.GetLaserDamage();
        laserFireRateSlider.value = PlayerModifierManager.Instance.GetLaserTime();
        laserRangeSlider.value = PlayerModifierManager.Instance.GetLaserRange();

        //Get missile values: NEEDS COMPLETING!
        missileDamageSlider.value = PlayerModifierManager.Instance.GetMissileDamage();
        missileAmmoCapacitySlider.value = PlayerModifierManager.Instance.GetMissileAmmoCapacity();
        missileReplenishRate.value = PlayerModifierManager.Instance.GetMissileReplenishRate();

        moveSpeedSlider.value = PlayerModifierManager.Instance.GetMoveSpeed();
        turnSpeedSlider.value = PlayerModifierManager.Instance.GetTurnSpeed();

        //Update Time
        timeLeftText.text = "Weeks remaining until next wave: " + timeLeft.ToString();
    }

    //SETTERS

    public void LaserFireRateIncreased()
    {
        if (timeLeft > 0)
        {
            PlayerModifierManager.Instance.CalculateModifier("laserTime", 0.1f);
            timeLeft -= 1.0f;
            UpdateAllVariables();
            
        }


    }

    public void LaserFireRangeIncreased()
    {
        if (timeLeft > 0)
        {
            PlayerModifierManager.Instance.CalculateModifier("laserRange", 0.1f);
            timeLeft -= 1.0f;
            UpdateAllVariables();
            
        }

    }

    public void LaserFireDamageIncreased()
    {
        if (timeLeft > 0)
        {
            PlayerModifierManager.Instance.CalculateModifier("laserDamage", 0.1f);
            timeLeft -= 1.0f;
            UpdateAllVariables();
            
        }

    }

    public void MoveSpeedIncreased()
    {
        if (timeLeft > 0)
        {
            PlayerModifierManager.Instance.CalculateModifier("moveSpeed", 0.1f);
            timeLeft -= 1.0f;
            UpdateAllVariables();
            
        }
    }

    public void TurnSpeedIncreased()
    {
        if (timeLeft > 0)
        {
            PlayerModifierManager.Instance.CalculateModifier("turnSpeed", 0.1f);
            timeLeft -= 1.0f;
            UpdateAllVariables();
            
        }
    }

    public void MissileFireDamageIncreased()
    {
        if (timeLeft > 0)
        {
            PlayerModifierManager.Instance.CalculateModifier("missileDamage", 0.1f);
            timeLeft -= 1.0f;
            UpdateAllVariables();

        }
    }

    public void MissileAmmoCapacityIncreased()
    {
        if (timeLeft > 0)
        {
            PlayerModifierManager.Instance.CalculateModifier("missileAmmo", 0.1f);
            timeLeft -= 1.0f;
            UpdateAllVariables();

        }
    }

    public void MissileFireReloadIncreased()
    {
        if (timeLeft > 0)
        {
            PlayerModifierManager.Instance.CalculateModifier("missileTime", 0.1f);
            timeLeft -= 1.0f;
            UpdateAllVariables();

        }
    }
}

