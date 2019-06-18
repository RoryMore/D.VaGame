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
    Outline outline;

    public bool Highlighted { get => highlighted; set => highlighted = value; }

    void Awake()
    {
//        waveManager = GameObject.Find("WaveManager");
//        enemyManager = waveManager.GetComponent<EnemyManager>();
        outline = GetComponent<Outline>();

        currentHealth = startingHealth;
    }


    void Update()
    {
        if (highlighted)
        {
            outline.OutlineColor = highlightColor;
        }
        else
        {
            outline.OutlineColor = defaultOutline;
        }
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