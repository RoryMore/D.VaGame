using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    PlayerHealth playerHealth;
    GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;
    public float killCount = 0.0f;
    Collider strafeArea;

    

    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    private void Awake()
    {
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        enemy = Resources.Load<GameObject>("SquidGwishin");
        strafeArea = GameObject.Find("StrafeArea").GetComponent<Collider>();
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
        for (int i = 0; i < 2; i++)
        {
            GameObject newEnemy = Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            newEnemy.GetComponent<GwishinMovement>().strafeArea = strafeArea;
            newEnemy.GetComponent<GwishinMovement>().heightTarget = strafeArea.bounds.center.y;
        }
        
    }

    public void Update()
    {
        
        
    }
}