using UnityEngine;
//Very Important to access UI components!
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public float startingHealth = 100;
    public float currentHealth;
    public Slider healthSlider;
    
    
    Animator anim;

    PlayerMovement playerMovement;

    GameObject torso;
    PlayerShooting playerShooting;

    LaserShooting laserShooting;
    MissleShooting missileShooting;

    //Audio
    AudioSource playerAudio;
    //Damaged system audio
    public AudioClip movementDamaged;
    public AudioClip systemBreached;
    public AudioClip criticalDamage;

    public AudioClip deathClip;

    


    bool isDead;
    bool damaged;

    float critHit;


    void Awake()
    {
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();

        playerShooting = GetComponentInChildren<PlayerShooting>();

        laserShooting = GetComponentInChildren<LaserShooting>();
        missileShooting = GetComponentInChildren<MissleShooting>();

        currentHealth = startingHealth;
    }


    void Update()
    {
        //Damage image lerp goes here if we want one
    }


    public void TakeDamage(float amount)
    {
        //Standard damage function
        damaged = true;
        currentHealth -= amount;

        //Generate damage chance number
        critHit = Random.Range(0.0f, 100.0f);
        //If the hit is above our current health, deal damage to a system
        if (critHit > currentHealth)
        {
            print("critical hit!");
            float choice = (Mathf.Round(Random.Range(0.0f, 3.0f)));

            if (choice == 0)
            {
                print("Move and Turn speed Reduced");
                //Updated singleton
                PlayerModifierManager.Instance.CalculateModifier("moveSpeed", -0.1f);
                PlayerModifierManager.Instance.CalculateModifier("turnSpeed", -0.1f);
                playerMovement.moveSpeedModifier = PlayerModifierManager.Instance.GetMoveSpeed();
                playerMovement.turnSpeedModifier = PlayerModifierManager.Instance.GetTurnSpeed();

                playerAudio.clip = movementDamaged;
                playerAudio.Play();
            }
            else if (choice == 1)
            {
                print("LaserGun Damage, Range reduced and Recharge time increased");
                PlayerModifierManager.Instance.CalculateModifier("laserTime", -0.1f);
                PlayerModifierManager.Instance.CalculateModifier("laserRange", -0.1f);
                PlayerModifierManager.Instance.CalculateModifier("laserDamage", -0.1f);
                laserShooting.laserTimeModifier = PlayerModifierManager.Instance.GetLaserTime();
                laserShooting.laserRangeModifier = PlayerModifierManager.Instance.GetLaserRange();
                laserShooting.laserDamageModifier = PlayerModifierManager.Instance.GetLaserDamage();

                playerAudio.clip = systemBreached;
                playerAudio.Play();


            }
            else if (choice == 2)
            {
                print("Missile amount, damage and refresh rate increased");
                PlayerModifierManager.Instance.CalculateModifier("missileDamage", -0.1f);
                PlayerModifierManager.Instance.CalculateModifier("missileAmmo", -0.1f);
                PlayerModifierManager.Instance.CalculateModifier("missileTime", -0.1f);
                missileShooting.missileDamageModifier = PlayerModifierManager.Instance.GetMissileDamage();
                missileShooting.missileAmmoModifier = PlayerModifierManager.Instance.GetMissileAmmoCapacity();
                missileShooting.missileReloadModifer = PlayerModifierManager.Instance.GetMissileReplenishRate();

                playerAudio.clip = criticalDamage;
                playerAudio.Play();


            }
            else if (choice == 3)
            {
                print("Machinegun damage and ammo count reduced, reload time increased");

            }
        }

        //healthSlider.value = currentHealth;

        //That big "oof" plays here
        //playerAudio.Play();

        //Death check 
        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }


    void Death()
    {
        isDead = true;

        //anim.SetTrigger("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play();

        //playerMovement.enabled = false;

        //SceneManager.LoadScene("GameOverScene");
    }


    public void RestartLevel()
    {
        SceneManager.LoadScene("GameLevel");
    }
}