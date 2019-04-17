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
    
    public AudioClip deathClip;
    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;

    GameObject torso;
    PlayerShooting playerShooting;


    bool isDead;
    bool damaged;

    float critHit;


    void Awake()
    {
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponentInChildren<PlayerShooting>();
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
            float choice = (Mathf.Round(Random.Range(0.0f, 7.0f)));

            

            if (choice == 0)
            {
                print("Movement Speed Reduced");
                //Updated singleton
                PlayerModifierManager.Instance.CalculateModifier("moveSpeed", -0.1f);
                playerMovement.moveSpeedModifier = PlayerModifierManager.Instance.GetMoveSpeed();
            }
            else if (choice == 1)
            {
                print("Turn Speed Reduced");
                PlayerModifierManager.Instance.CalculateModifier("moveSpeed", -0.1f);
                playerMovement.turnSpeedModifier = PlayerModifierManager.Instance.GetTurnSpeed();
                
            }
            else if (choice == 2)
            {
                print("Laser recharge time increased");
                PlayerModifierManager.Instance.CalculateModifier("laserTime", -0.1f);
                playerShooting.laserTimeModifier = PlayerModifierManager.Instance.GetLaserTime();
                
            }
            else if (choice == 3)
            {
                print("Laser range reduced");
                PlayerModifierManager.Instance.CalculateModifier("laserRange", -0.1f);
                playerShooting.laserRangeModifier = PlayerModifierManager.Instance.GetLaserRange();
            }
            else if (choice == 4)
            {
                print("Laser damage reduced");
                PlayerModifierManager.Instance.CalculateModifier("laserDamage", -0.1f);
                playerShooting.laserDamageModifier = PlayerModifierManager.Instance.GetLaserDamage();
                    
            }
            else if (choice == 5)
            {
                print("Missile damage reduced");
                PlayerModifierManager.Instance.CalculateModifier("missileDamage", -0.1f);
                //Update missile damage modifier
            }
            else if (choice == 6)
            {
                print("Missile Ammo reduced");
                PlayerModifierManager.Instance.CalculateModifier("missileAmmo", -0.1f);
                //Update missile ammo modifier
            }
            else if (choice == 7)
            {
                print("Missile rate reduced");
                PlayerModifierManager.Instance.CalculateModifier("missileTime", -0.1f);
                //Update missile damage modifier
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

        //playerAudio.clip = deathClip;
        //playerAudio.Play();

        //playerMovement.enabled = false;

        SceneManager.LoadScene("GameOverScene");
    }


    public void RestartLevel()
    {
        SceneManager.LoadScene("GameLevel");
    }
}