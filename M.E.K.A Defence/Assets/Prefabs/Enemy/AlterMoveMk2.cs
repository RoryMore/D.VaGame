using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlterMoveMk2 : MonoBehaviour
{
    //var for round action
    public float _radius_length;
    public float _angle_speed;

    private float temp_angle;

    private Vector3 _pos_new;

    public Vector3 _center_pos;

    public bool _round_its_center;
    public Transform player;
    bool enableRound = false;
    //var for seek action
    public float speed;

    
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;


    void Awake()
    {

        player = GameObject.Find("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
    // Use this for initialization
    void Start()
    {
        if (_round_its_center)
        {
            _center_pos = transform.localPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        _center_pos = player.localPosition;
        temp_angle += _angle_speed * Time.deltaTime;

        _pos_new.x = _center_pos.x + Mathf.Cos(temp_angle) * _radius_length;
        _pos_new.y = transform.localPosition.y;
        _pos_new.z = _center_pos.z + Mathf.Sin(temp_angle) * _radius_length;

        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            nav.SetDestination(player.position);
        }
        else
        {
            nav.enabled = false;
            enableRound = false;
        }
        if(Vector3.Distance(transform.localPosition,_pos_new) <= _radius_length && enableRound == false)
        {
            nav.enabled = false;
            enableRound = true;
            _pos_new = transform.position;
        }    
       
  
        if(Vector3.Distance(transform.localPosition, _pos_new) > _radius_length)
        {
            nav.enabled = true;
            enableRound = false;
        }


        if (enableRound == true)
        {
            transform.localPosition = _pos_new;
        }
        
    }
}
