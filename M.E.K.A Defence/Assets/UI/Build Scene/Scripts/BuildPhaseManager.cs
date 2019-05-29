using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BuildPhaseManager : MonoBehaviour
{
    Slider laserHealthSlider; 
    Slider missileHealthSlider;
    Slider movementHealthSlider;


    //Time Counting Variables
    public Text timeLeftText;
    private float timeLeft = 4;

    //Min and max are 0 and 1 respectivley, start at 1
    //Set value to be multiplier

    //Plus button for each slider
    //If plus button clicked, -1 week from time resource and + 0.1 to value
    //Only allow button to run if week != 0



    private void Awake()
    {
        //Laser Sliders
        laserHealthSlider = GameObject.Find("LaserGunHealthSlider").GetComponent<Slider>();

        //Missile Sliders
        missileHealthSlider = GameObject.Find("MissilePodHealthSlider").GetComponent<Slider>();

        //Move Sliders
        movementHealthSlider = GameObject.Find("MovementHealthSlider").GetComponent<Slider>();

        //Time left Text
        timeLeftText = GameObject.Find("TimeLeftText").GetComponent<Text>();

        //Set the text to be what it needs to be from the previous scene
        UpdateAllVariables();
    }
    public void StartWave()
    {
        //Play Game function
        SceneManager.LoadScene("ShmupScene");
    }

    private void UpdateAllVariables()
    {
        //All values are increased and decreased together at a rate of 10%. Checking any variable is the same as check
        laserHealthSlider.value = PlayerModifierManager.Instance.GetLaserGunHealth();

        //Get missile values: NEEDS COMPLETING!
        missileHealthSlider.value = PlayerModifierManager.Instance.GetMissileHealth();

        movementHealthSlider.value = PlayerModifierManager.Instance.GetMovementHealth();

        //Update Time
        timeLeftText.text = "Weeks remaining until next wave: " + timeLeft.ToString();
    }

    //SETTERS


    public void LaserGunRepair()
    {
        if (timeLeft > 0)
        {
            PlayerModifierManager.Instance.CalculateModifier("laserTime", 0.1f);
            PlayerModifierManager.Instance.CalculateModifier("laserRange", 0.1f);
            PlayerModifierManager.Instance.CalculateModifier("laserDamage", 0.1f);

            PlayerModifierManager.Instance.CalculateModifier("laserGunHealth", 0.1f);
            timeLeft -= 1.0f;
            UpdateAllVariables();

        }
    }

    public void MovementRepair()
    {
        if (timeLeft > 0)
        {
            PlayerModifierManager.Instance.CalculateModifier("moveSpeed", 0.1f);
            PlayerModifierManager.Instance.CalculateModifier("turnSpeed", 0.1f);
            PlayerModifierManager.Instance.CalculateModifier("movementHealth", 0.1f);
            timeLeft -= 1.0f;
            UpdateAllVariables();
            
        }
    }

    public void MissilePodRepair()
    {
        if (timeLeft > 0)
        {
            //Damage
            PlayerModifierManager.Instance.CalculateModifier("missileDamage", 0.1f);
            //Ammo Capacity
            PlayerModifierManager.Instance.CalculateModifier("missileAmmo", 0.1f);
            //Reload time
            PlayerModifierManager.Instance.CalculateModifier("missileTime", 0.1f);

            PlayerModifierManager.Instance.CalculateModifier("missileHealth", 0.1f);
            timeLeft -= 1.0f;
            UpdateAllVariables();

        }
    }
}

