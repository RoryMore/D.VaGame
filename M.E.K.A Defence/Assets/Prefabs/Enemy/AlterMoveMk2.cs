using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlterMoveMk2 : MonoBehaviour
{
    //var for round action
    public float _radius_length;
    public float _angle_speed;
    public float angleDiff;

    public float temp_angle;

    private Vector3 _pos_new;

    public Vector3 _center_pos;

    public bool _round_its_center;
    public Transform player;
    public bool enableRound = false;
    //var for seek action
    public float speed;

    
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;


    void Awake()
    {
        _radius_length = gameObject.GetComponent<SphereCollider>().radius;
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


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            nav.enabled = false;
            enableRound = true;
        }

    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            nav.enabled = true;
            enableRound = false;
        }
        }

    // Update is called once per frame
    void Update()
    {
        angleDiff = Vector3.Angle(_center_pos, gameObject.transform.position);

        _center_pos = player.position;

        temp_angle += _angle_speed * Time.deltaTime;

        _pos_new.x = _center_pos.x + Mathf.Cos(temp_angle) * _radius_length;
        _pos_new.y = transform.localPosition.y;
        _pos_new.z = _center_pos.z + Mathf.Sin(temp_angle) * _radius_length;

        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0 && !enableRound)
        {
            nav.SetDestination(player.position);
        }
        else
        {
            //nav.enabled = false;
            //enableRound = false;
        }
        if(Vector3.Distance(transform.localPosition,_pos_new) <= _radius_length && enableRound == false)
        {
            angleDiff = Vector3.Angle(_center_pos, gameObject.transform.position);
            //temp_angle = angleDiff / 360;
            //nav.enabled = false;
            //enableRound = true;
            _pos_new = transform.position;
        }    
       
  
        //if(Vector3.Distance(transform.localPosition, _pos_new) > _radius_length)
        //{
        //    enableRound = false;
        //}



        if (enableRound == true)
        {
            transform.RotateAround(_center_pos, Vector3.up, _angle_speed * Time.deltaTime);

            //transform.Rotate(_center_pos, temp_angle);

            //transform.localPosition = _pos_new;
        }
        
    }
}
