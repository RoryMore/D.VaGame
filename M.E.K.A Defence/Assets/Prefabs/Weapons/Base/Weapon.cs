using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

enum MouseButton
{
    None = -1,
    Left = 0,
    Right = 1,
    Middle = 2
}

public class Weapon : MonoBehaviour
{
    [SerializeField]  WeaponStats stats = null;
    [SerializeField] KeyCode useKey = KeyCode.None;
    [SerializeField] MouseButton useButton = MouseButton.None;
    [SerializeField] Canvas UI = null;
    [SerializeField] Vector3 offsetUI = Vector3.zero;

    WeaponUI weaponUI = null;
    float smoothRate = 0.1f;

    bool canFire = true;
    bool charging = false;
    bool firing = false;
    bool aquiring = false;

    // ----- Sound ----- //
    AudioSource source;

    //-----UI - -
    Vector3 originalUIDirection;
    TextMeshProUGUI ammoText;

    protected WeaponStats Stats { get => stats; set => stats = value; }
    protected KeyCode UseKey { get => useKey; set => useKey = value; }
    internal MouseButton UseButton { get => useButton; set => useButton = value; }
    protected bool CanFire { get => canFire; set => canFire = value; }
    protected WeaponUI WeaponUI { get => weaponUI; set => weaponUI = value; }
    public AudioSource Source { get => source; set => source = value; }
    public bool Charging { get => charging; set => charging = value; }
    public bool Firing { get => firing; set => firing = value; }
    public bool Aquiring { get => aquiring; set => aquiring = value; }

    private void Start()
    {
        SetupUI();
        source = gameObject.AddComponent<AudioSource>();
        stats.CurrentAmmo = stats.AmmoCapacity;

        StartCoroutine(WeaponCooldown());
        StartCoroutine(AmmoReplenish());
    }

    private void FixedUpdate()
    {
        UpdateUI();

        if (PlayerActivatesInput())
        {
            weaponUI.Activated = true;
        }
    }



    IEnumerator WeaponCooldown()
    {
        while (true)
        {
            if (stats.CurrentAmmo > 0 && !charging && !firing && !aquiring && !canFire)
            {
                canFire = true;
                yield return new WaitForSeconds(stats.FireRate);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator AmmoReplenish()
    {
        while (true)
        {
            if (stats.CurrentAmmo < stats.AmmoCapacityBase && !charging && !firing && !aquiring)
            {
                stats.CurrentAmmo += stats.ReplenishAmount;
                if (stats.CurrentAmmo > stats.AmmoCapacityBase)
                {
                    stats.CurrentAmmo = stats.AmmoCapacityBase;
                }
                weaponUI.UpdateUICounter(stats.CurrentAmmo);
                yield return new WaitForSeconds(stats.ReplenishRateBase);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    protected IEnumerator WeaponCharge(float chargeTime)
    {
        charging = true;
        if (stats.Sounds.ChargingClip)
        {
            float clipLength = stats.Sounds.ChargingClip.length;
            
            if (clipLength < chargeTime)
            {
                yield return new WaitForSeconds(chargeTime - clipLength);
            }
            source.clip = stats.Sounds.ChargingClip;
            source.Play();
            yield return new WaitForSeconds(clipLength);
        }
        else
        {
            yield return new WaitForSeconds(chargeTime);
        }
        charging = false;
    }

    void SetupUI()
    {
        if (!UI) return;

        weaponUI = UI.GetComponent<WeaponUI>();
        weaponUI.SetupUI(stats);
        originalUIDirection = -UI.transform.forward;
        offsetUI = UI.transform.position - transform.position;
        ammoText = UI.GetComponentInChildren<TextMeshProUGUI>();
    }

    void UpdateUI()
    {
        if (UI)
        {
            MoveUI();
            RotateUI();
            UpdateUIText();
        }
    }

    void MoveUI()
    {
        Vector3 smoothedPosition = Vector3.Lerp(UI.transform.position, transform.position + offsetUI, smoothRate);
        UI.transform.position = smoothedPosition;
    }

    void RotateUI()
    {
        UI.transform.LookAt(transform.position);
    }

    void UpdateUIText()
    {
        ammoText.text = stats.CurrentAmmo + "/" + stats.AmmoCapacity;
    }

    protected void ReduceCurrentAmmo(int ammoRemoved)
    {
        stats.CurrentAmmo -= ammoRemoved;
        weaponUI.UpdateUICounter(stats.CurrentAmmo);
    }

    //----- Player Inputs -----//

    protected bool PlayerActivatesInput()
    {
        //----- If the player presses the use key
        if (Input.GetKeyDown(UseKey)) return true;

        //----- If there is no mouse button
        if (UseButton == MouseButton.None) return false;

        //----- if the player presses the mouse button
        if (Input.GetMouseButtonDown((int)UseButton)) return true;

        //-----there is no input so return false
        return false;
    }

    protected bool PlayerDeactivatesInput()
    {
        //----- If the player lets go of the use key
        if (Input.GetKeyUp(UseKey)) return true;

        //----- If there isnt a mouse button 
        if (UseButton == MouseButton.None) return false;

        //----- if the player lets go of the Mouse button
        if (Input.GetMouseButtonUp((int)UseButton)) return true;

        //-----there is no input so return false
        return false;
    }

    protected bool PlayerHoldsInput()
    {
        //----- If the player lets go of the use key
        if (Input.GetKey(UseKey)) return true;

        //----- If there isnt a mouse button 
        if (UseButton == MouseButton.None) return false;

        //----- if the player lets go of the Mouse button
        if (Input.GetMouseButton((int)UseButton)) return true;

        //-----there is no input so return false
        return false;
    }

    //----- ------------- -----//

}
