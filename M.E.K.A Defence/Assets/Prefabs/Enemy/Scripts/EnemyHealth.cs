using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHealth : MonoBehaviour
{
    public float startingHealth = 100;
    public float currentHealth;
    public float sinkSpeed = 2.5f;
    //public int scoreValue = 10;
    public AudioClip deathClip;


    GameObject waveManager;
    EnemyManager enemyManager;

    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    Collider capsuleCollider;
    public bool isDead;
    bool isSinking;
    public bool killedByMissle = false;

    bool highlighted = false;
    Color highlightColor = Color.red;
    Color defaultOutline = Color.blue;
    Outline outline;

    public bool Highlighted { get => highlighted; set => highlighted = value; }

    void Awake()
    {
        //anim = GetComponent<Animator>();
        //enemyAudio = GetComponent<AudioSource>();
       //hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<Collider>();
        waveManager = GameObject.Find("WaveManager");
        enemyManager = waveManager.GetComponent<EnemyManager>();

        currentHealth = startingHealth;
    }


    void Update()
    {
        if (isSinking)
        {
            //Sinks into the water! Splash Effect?
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }

    //Function that is called when the player deals damage to you
    //Default condition format
    public void TakeDamage(float amount, Vector3 hitPoint)
    {
        if (isDead)
            return;

        //Audio Cue
        //enemyAudio.Play();

        currentHealth -= amount;

        //Hit particles played here
        //hitParticles.transform.position = hitPoint;
        //hitParticles.Play();

        if (currentHealth <= 0)
        {
            Death();
        }
    }



    void Death()
    {
        isDead = true;

        //maybe change this to disable collider
        
        StartSinking();

        //anim.SetTrigger("Dead");

        //enemyAudio.clip = deathClip;
        //enemyAudio.Play();
    }


    public void StartSinking()
    {
        //GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        //GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;

        //Counts enemy towards wave completion
        //enemyManager.killCount += 1.0f;
        //enemyManager.GetComponent<EnemyManager>().killCount += 1.0f;

        enemyManager.killCount += 1.0f;

        //print("Its here!");

        //Delete the object once it sinks below the surface

        Destroy(gameObject, 10f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && other.gameObject.GetComponent<EnemyHealth>().isDead == true)
        {
            TakeDamage(Random.Range(50, 100), other.transform.position);
        }
    }
}