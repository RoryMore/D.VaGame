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
    

    List<Image> ammoCounters = new List<Image>();
    WeaponStats stats;

    bool setup = false;
    int currentAmmo;

    Vector3 originalPosition;

    private void Update()
    {
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
