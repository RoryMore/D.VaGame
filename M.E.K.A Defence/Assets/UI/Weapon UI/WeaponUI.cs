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
    [SerializeField] Sprite imageToSet = null;
    [SerializeField] GameObject image = null;
    [SerializeField] AudioClip activateClip = null;
    [SerializeField] AudioClip deactivateClip = null;

    Animator anim = null;
    List<Image> ammoCounters = new List<Image>();
    WeaponStats stats;
    AudioSource source;



    bool setup = false;
    int currentAmmo;

    [SerializeField] float maxIdleTime = 2f;
    float timeSinceInput = 0;

    Vector3 originalPosition;

    public float TimeSinceInput { get => timeSinceInput; set => timeSinceInput = value; }
    public Animator Anim { get => anim; set => anim = value; }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        image.GetComponent<Image>().sprite = imageToSet;
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        CalculateTimeSinceInput();
        if (timeSinceInput > maxIdleTime && stats.CurrentAmmo >= stats.AmmoCapacity && anim.GetBool("online"))
        {
            anim.SetBool("online", false);
            source.clip = deactivateClip;
            source.Play();
        }
        else if(timeSinceInput <= 0 && !anim.GetBool("online"))
        {
            anim.SetBool("online", true);
            source.clip = activateClip;
            source.Play();
        }
    }

    void CalculateTimeSinceInput()
    {
            timeSinceInput += Time.deltaTime;
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
