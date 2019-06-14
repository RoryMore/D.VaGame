using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] GameObject ammoContainer = null;
    [SerializeField] Sprite fullAmmoContainer = null;
    [SerializeField] Sprite emptyAmmoContainer = null;
    [SerializeField] GameObject ammoCounter = null;

    Animator anim = null;
    List<Image> ammoCounters = new List<Image>();
    WeaponStats stats;

    bool setup = false;
    int currentAmmo;

    [SerializeField] float maxIdleTime = 2f;
    float timeSinceInput = 0;
    bool activated = false;

    Vector3 originalPosition;

    public bool Activated { get => activated; set => activated = value; }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        CalculateTimeSinceInput();

        print(stats.CurrentAmmo + " / " + stats.AmmoCapacity);
        if (timeSinceInput > maxIdleTime && stats.CurrentAmmo == stats.AmmoCapacity)
        {
            anim.SetBool("online", false);
            activated = false;
        }
        else if (timeSinceInput <= 0) anim.SetBool("online", true);
    }

    void CalculateTimeSinceInput()
    {
        if (activated)
        {
            timeSinceInput = 0;
        }
        else
        {
            timeSinceInput += Time.deltaTime;
            
        }
    }

    public void UpdateUICounter(int newCurrentAmmo)
    {
        currentAmmo = newCurrentAmmo;

        for (int i = 0; i < ammoCounters.Count; i++)
        {
            if (i + 1 <= currentAmmo) ammoCounters[i].sprite = fullAmmoContainer;
            else ammoCounters[i].sprite = emptyAmmoContainer;
        }

    }

    public void SetupUI(WeaponStats stats)
    {

        this.stats = stats;

        for (int i = 0; i < stats.AmmoCapacity; i++)
        {
            GameObject newCounter = Instantiate(ammoCounter, ammoContainer.transform);
            ammoCounters.Add(newCounter.GetComponent<Image>());
        }

        UpdateUICounter(stats.AmmoCapacity);

        setup = true;
    }
}
