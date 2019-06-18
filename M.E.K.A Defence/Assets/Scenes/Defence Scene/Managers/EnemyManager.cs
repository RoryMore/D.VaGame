using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    PlayerHealth playerHealth;
    [SerializeField] GameObject enemy = null;
    public float spawnTime = 10.0f;
    public float killCount = 0.0f;
    Collider strafeArea;
    Collider spawnArea;

    

    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    private void Awake()
    {
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        strafeArea = GameObject.Find("StrafeArea").GetComponent<Collider>();
        spawnArea = GetComponent<Collider>();
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
            ///Broadcast to the enemies to begin their retreat
            SceneManager.LoadScene("BuildPhaseScene");
        }
        //This spawns an enemy at one of the spawn points randomly every spawn time seconds that pass


        for (int i = 0; i < Random.Range(1, PlayerModifierManager.Instance.GetWaveCount() * 5); i++)
        {
            Vector3 spawnPosition;
            spawnPosition.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            spawnPosition.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            spawnPosition.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);
            Quaternion spawnRotation = Random.rotation;

            GameObject newEnemy = Instantiate(enemy, spawnPosition, spawnRotation);
            newEnemy.GetComponent<GwishinMovement>().strafeArea = strafeArea;
            newEnemy.GetComponent<GwishinMovement>().heightTarget = strafeArea.bounds.center.y;
        }
        
    }

    public void Update()
    {
        
        
    }
}