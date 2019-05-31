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
    int currentAmmo = 0;
    float smoothRate = 0.1f;
    bool canFire = true;

    //-----UI - -
    Vector3 originalUIDirection;
    TextMeshProUGUI ammoText;

    protected WeaponStats Stats { get => stats; set => stats = value; }
    protected KeyCode UseKey { get => useKey; set => useKey = value; }
    protected int CurrentAmmo { get => currentAmmo; set => currentAmmo = value; }
    internal MouseButton UseButton { get => useButton; set => useButton = value; }
    protected bool CanFire { get => canFire; set => canFire = value; }

    private void Start()
    {
        SetupUI();

        currentAmmo = stats.AmmoCapacity;

        StartCoroutine(WeaponCooldown());
        StartCoroutine(AmmoReplenish());
    }

    private void FixedUpdate()
    {
        UpdateUI();
    }



    IEnumerator WeaponCooldown()
    {
        while (true)
        {
                if (currentAmmo > 0)
                {
                    canFire = true;
                }
            yield return new WaitForSeconds(stats.FireRate);
        }
    }

    IEnumerator AmmoReplenish()
    {
        while (true)
        {
            if (currentAmmo < stats.AmmoCapacity)
            {
                currentAmmo += stats.ReplenishAmount;
                if (currentAmmo > stats.AmmoCapacity)
                {
                    currentAmmo = stats.AmmoCapacity;
                }
                weaponUI.UpdateUICounter(currentAmmo);
            }
            yield return new WaitForSeconds(stats.ReplenishRate);
        }
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
        ammoText.text = currentAmmo + "/" + stats.AmmoCapacity;
    }



}
