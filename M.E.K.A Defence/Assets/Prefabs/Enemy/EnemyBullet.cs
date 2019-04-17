using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    float spd = 7f;
    Rigidbody rb;
    public GameObject target;
    Vector3 movedir;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        movedir = (target.transform.position - transform.position).normalized * spd;
        rb.velocity = new Vector3(movedir.x, movedir.y, movedir.z);
        //Destroy(gameObject, 3.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
