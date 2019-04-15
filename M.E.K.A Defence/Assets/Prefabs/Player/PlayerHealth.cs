using UnityEngine;
//Very Important to access UI components!
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    
    public AudioClip deathClip;
    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;


    bool isDead;
    bool damaged;

    float critHit;


    void Awake()
    {
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        currentHealth = startingHealth;
    }


    void Update()
    {
        //Damage image lerp goes here if we want one
    }


    public void TakeDamage(int amount)
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
            //Random.Range(0.0f, 8.0f);

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
    }


    public void RestartLevel()
    {
        SceneManager.LoadScene("GameLevel");
    }
}