using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHealth : MonoBehaviour
{
    public float startingHealth = 100;
    public float currentHealth;
    GameObject waveManager;
    EnemyManager enemyManager;
    public bool dead;
    public bool killedByMissle = false;

    bool highlighted = false;
    Color highlightColor = Color.red;
    Color defaultOutline = Color.blue;

    public bool Highlighted { get => highlighted; set => highlighted = value; }

    void Awake()
    {
//        waveManager = GameObject.Find("WaveManager");
//        enemyManager = waveManager.GetComponent<EnemyManager>();

        currentHealth = startingHealth;
    }


    void Update()
    {
    }

    //Function that is called when the player deals damage to you
    //Default condition format
    public void TakeDamage(float amount, Vector3 hitPoint)
    {
        if (dead)
            return;

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            GetComponent<Gwishin>().Die();
            dead = true;
            GetComponent<EnemyLaserWeapon>().Dead = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && dead == true)
        {
            TakeDamage(Random.Range(50, 100), other.transform.position);
        }
    }
}