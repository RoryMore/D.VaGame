using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;

    public float killCount = 0.0f;


    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }


    void Spawn()
    {
        //If player dies
        if (playerHealth.currentHealth <= 0f)
        {
            SceneManager.LoadScene("GameOverScene");
            //return;
        }

        //A magic number equation that ramps up the number of required kills between eah wave
        if (killCount > (10.0f + (PlayerModifierManager.Instance.GetWaveCount() * 10)))
        {
            SceneManager.LoadScene("BuildPhaseScene");
        }

            //This spawns an enemy at one of the spawn points randomly every spawn time seconds that pass
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }

    public void Update()
    {
        
        
    }
}