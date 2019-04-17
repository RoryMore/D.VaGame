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
        missileAmmoCapacitySlider = GameObject.Find("MissilePodFireRateSlider").GetComponent<Slider>();
        missileReplenishRate = GameObject.Find("MissilePodFireRangeSlider").GetComponent<Slider>();

        //Move Sliders
        moveSpeedSlider = GameObject.Find("WalkSpeedSlider").GetComponent<Slider>();
        turnSpeedSlider = GameObject.Find("TurnSpeedSlider").GetComponent<Slider>();

    }
    public void StartWave()
    {
        //CHANGE THIS TO GAME SCENE
        SceneManager.LoadScene("ShmupScene");
    }

    private void Update()
    {
        laserDamageSlider.value = 0.5f;
        laserFireRateSlider.value = 0.5f;
        laserRangeSlider.value = 0.5f;
    }

}

