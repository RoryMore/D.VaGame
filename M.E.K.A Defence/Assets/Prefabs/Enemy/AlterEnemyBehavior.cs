using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlterEnemyBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public float _radius_length;
    public float _angle_speed;

    private float temp_angle;

    private Vector3 _pos_new;

    public Vector3 _center_pos;
    public Transform target;
    public bool _round_its_center = false;
    void Start()
    {
        if (_round_its_center)
        {
            _center_pos = transform.localPosition;
        }
        else
        {
            _center_pos = target.localPosition;
        }
    }
    //Keep a range distance between player, face the player, and shoot bullet at player

    // Update is called once per frame
    void Update()
    {
        _center_pos = target.localPosition;
        temp_angle += _angle_speed * Time.deltaTime; // 

        _pos_new.x = _center_pos.x + Mathf.Cos(temp_angle) * _radius_length;
        _pos_new.y = transform.localPosition.y ;
        _pos_new.z = _center_pos.z + Mathf.Sin(temp_angle) * _radius_length;

        transform.localPosition = _pos_new;
    }
}
