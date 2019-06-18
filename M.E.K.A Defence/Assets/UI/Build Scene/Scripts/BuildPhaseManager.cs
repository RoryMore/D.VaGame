using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BuildPhaseManager : MonoBehaviour
{
    //Display values
    Slider laserHealthSlider; 
    Slider missileHealthSlider;
    Slider movementHealthSlider;

    public Text laserDescription;

    float startingLaserHealth;
    float startingMissileHealth;
    float startingMovementHealth;


    //Time Counting Variables
    public Text timeLeftText;
    private float timeLeft = 4;

    // For changing between repair and upgrade
    public GameObject repairCanvas;
    public GameObject upgradeCanvas;

    //Min and max are 0 and 1 respectivley, start at 1
    //Set value to be multiplier

    //Plus button for each slider
    //If plus button clicked, -1 week from time resource and + 0.1 to value
    //Only allow button to run if week != 0



    private void Awake()
    {
        //Laser Values
        laserHealthSlider = GameObject.Find("LaserGunHealthSlider").GetComponent<Slider>();
        float startingLaserHealth = PlayerModifierManager.Instance.GetLaserGunHealth();
        laserDescription = GameObject.Find("LaserDescription").GetComponent<Text>();

        //Missile Values
        missileHealthSlider = GameObject.Find("MissilePodHealthSlider").GetComponent<Slider>();
        float startingMissileHealth = PlayerModifierManager.Instance.GetMissileHealth();
     

        //Movement Values
        movementHealthSlider = GameObject.Find("MovementHealthSlider").GetComponent<Slider>();
        float startingMovementHealth = PlayerModifierManager.Instance.GetMovementHealth();



        //Time left Text
        timeLeftText = GameObject.Find("TimeLeftText").GetComponent<Text>();
        //Set the text to be what it needs to be from the previous scene

        repairCanvas = GameObject.Find("RepairsCanvas");
        upgradeCanvas = GameObject.Find("UpgradesCanvas");

        //Start the player in Repairs menu unless they have no damage to any system, in which case, automatically send them to the upgrade screen, as its the only thing they
        //May wish to do
        if (PlayerModifierManager.Instance.GetLaserGunHealth() == 1.0f && PlayerModifierManager.Instance.GetMissileHealth() == 1.0f && PlayerModifierManager.Instance.GetMovementHealth() == 1.0f)
        {
            GoToUpgrades();
        }
        else
        {
            GoToRepairs();
        }
        


        UpdateAllVariables();

    }
    public void StartWave()
    {
        //Won't let you leave the repair bay unless all your time is spent, or you have fully repaired all parts of your Meka
        if (timeLeft == 0 || PlayerModifierManager.Instance.GetLaserGunHealth() == 1.0f && PlayerModifierManager.Instance.GetMissileHealth() == 1.0f && PlayerModifierManager.Instance.GetMovementHealth() == 1.0f)
        {
            //Play Game function
            PlayerModifierManager.Instance.UpdateWeaponStats();
            SceneManager.LoadScene("Final Scene");
        }

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

        //Update Descriptions
        laserDescription.text = "A ramshackle laser cannon made by strapping 8 unstable prototypes in a single housing and focusing them with an impure crystal. Currently opperating at " + (PlayerModifierManager.Instance.GetLaserGunHealth() * 100).ToString() + "% efficiency, reducing your recharge time, range and damage output by " + (100 - (PlayerModifierManager.Instance.GetLaserGunHealth() * 100)).ToString() + "%";
    }

    //SETTERS


    public void LaserGunRepair()
    {
        if (timeLeft > 0 && PlayerModifierManager.Instance.GetLaserGunHealth() != 1.0f)
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
        if (timeLeft > 0 && PlayerModifierManager.Instance.GetMovementHealth() != 1.0f)
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
        if (timeLeft > 0 && PlayerModifierManager.Instance.GetMissileHealth() != 1.0f)
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

    public void LaserGunUndoRepair()
    {
        //This logic will need updated if we change the time between waves for any reason
        //Checks to see if we don't have the maximum number of days left
        //we aren't reducing our value to below when it started
        //and that we aren't reducing to below 0

        if (timeLeft != 4 && PlayerModifierManager.Instance.GetLaserGunHealth() > startingLaserHealth && PlayerModifierManager.Instance.GetLaserGunHealth() != 0.0f)
        {
            PlayerModifierManager.Instance.CalculateModifier("laserTime", -0.1f);
            PlayerModifierManager.Instance.CalculateModifier("laserRange", -0.1f);
            PlayerModifierManager.Instance.CalculateModifier("laserDamage", -0.1f);

            PlayerModifierManager.Instance.CalculateModifier("laserGunHealth", -0.1f);
            timeLeft += 1.0f;
            UpdateAllVariables();
        }
    }

    public void MovementUndoRepair()
    {
        if (timeLeft != 4 && PlayerModifierManager.Instance.GetMovementHealth() > startingMovementHealth && PlayerModifierManager.Instance.GetMovementHealth() != 0.0f)
        {
            PlayerModifierManager.Instance.CalculateModifier("moveSpeed", -0.1f);
            PlayerModifierManager.Instance.CalculateModifier("turnSpeed", -0.1f);
            PlayerModifierManager.Instance.CalculateModifier("movementHealth", -0.1f);
            timeLeft += 1.0f;
            UpdateAllVariables();
        }
    }

    public void MissileUndoRepair()
    {
        if (timeLeft != 4 && PlayerModifierManager.Instance.GetMissileHealth() > startingMissileHealth && PlayerModifierManager.Instance.GetMissileHealth() != 0.0f)
        {
            //Damage
            PlayerModifierManager.Instance.CalculateModifier("missileDamage", -0.1f);
            //Ammo Capacity
            PlayerModifierManager.Instance.CalculateModifier("missileAmmo", -0.1f);
            //Reload time
            PlayerModifierManager.Instance.CalculateModifier("missileTime", -0.1f);

            PlayerModifierManager.Instance.CalculateModifier("missileHealth", -0.1f);
            timeLeft += 1.0f;
            UpdateAllVariables();
        }

    }

    public void GetDashUpgrade()
    {
        if (timeLeft == 4 && PlayerModifierManager.Instance.GetMovementHealth() == 0.2f)
        {
            PlayerModifierManager.Instance.UpgradeDash();
            timeLeft -= 4.0f;
            UpdateAllVariables();
        }

    }

    public void GoToRepairs()
    {
        repairCanvas.SetActive(true);
        upgradeCanvas.SetActive(false);
    }

    public void GoToUpgrades()
    {
        repairCanvas.SetActive(false);
        upgradeCanvas.SetActive(true);
    }
}

