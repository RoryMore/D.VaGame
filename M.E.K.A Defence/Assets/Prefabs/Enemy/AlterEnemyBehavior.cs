using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class AlterEnemyBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    private NavMeshAgent myNavMeshAgent;
    private float checkrate;
    private float nextCheck;
    private float wanderRange = 20;
    private Transform myTransform;
    private NavMeshHit navHit;
    private Vector3 wanderTarget;
    void Start()
    {
        //SceneManager.LoadSceneAsync("NavMeshTestScene");
    }
    //Keep a range distance between player, face the player, and shoot bullet at player

    // Update is called once per frame
    private void OnEnable()
    {
        Init();

    }
    private void OnDisable()
    {
        
    }
    void Update()
    {
        if(Time.time >nextCheck)
        {
            nextCheck = Time.time + checkrate;
            checkIfShouldWander();
        }
    }

    void Init()
    {
           myNavMeshAgent = GetComponent<NavMeshAgent>();
   
        
        checkrate = Random.Range(0.3f, 0.4f);
        myTransform = transform;
    }
    void checkIfShouldWander() {
        if(Vector3.Distance(transform.position,player.transform.position) < 6.0f)
        {
            if(RandomWanderTarget(myTransform.position,wanderRange,out wanderTarget))
            {
                myNavMeshAgent.SetDestination(wanderTarget);
                
            }
        }
    }
    bool RandomWanderTarget(Vector3 centre,float range,out Vector3 result)
    {
        Vector3 randomPoint = centre + Random.insideUnitSphere * wanderRange;
        while(Vector3.Distance(player.transform.position,transform.position) < 6.0f)
        {
           randomPoint = centre + Random.insideUnitSphere * wanderRange;
        }
        if (NavMesh.SamplePosition(randomPoint, out navHit, 1.0f, NavMesh.AllAreas))
        {
            result = navHit.position;
            return true;
        }
        else
        {
            result = centre;
            return false;
        }
      
    }
    //void disableThis()
    //{
    //    this.enabled = false;
    //}
}
