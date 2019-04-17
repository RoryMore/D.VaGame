using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAltFire : MonoBehaviour
{
   
   public GameObject bullet;
    public GameObject player;
    float fireRate, nextfire;
    bool canfire = false;

    // Start is called before the first frame update
    void Start()
    {
        fireRate = 1.2f;
        nextfire = Time.time;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
          
           canfire = true;
        }

    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            
            canfire = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        checkfire();
    }
    void checkfire()
    {
        if(Time.time >nextfire && canfire)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            nextfire = Time.time + fireRate;
        }
    }
}
