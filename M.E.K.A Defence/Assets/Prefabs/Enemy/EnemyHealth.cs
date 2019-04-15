﻿using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    //public int scoreValue = 10;
    public AudioClip deathClip;


    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;


    void Awake()
    {
        //anim = GetComponent<Animator>();
        //enemyAudio = GetComponent<AudioSource>();
       //hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();

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
    public void TakeDamage(int amount, Vector3 hitPoint)
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

        capsuleCollider.isTrigger = true;

        //anim.SetTrigger("Dead");

        //enemyAudio.clip = deathClip;
        //enemyAudio.Play();
    }


    public void StartSinking()
    {
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        //ScoreManager.score += scoreValue;
        //Delete the object once it sinks below the surface
        Destroy(gameObject, 2f);
    }
}