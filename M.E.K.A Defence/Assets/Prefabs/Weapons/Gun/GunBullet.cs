using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBullet : MonoBehaviour
{
    float speed;
    float damage;
    float lifeTime = 10;

    public void SetupBullet(float speed, float damage)
    {
        this.speed = speed;
        this.damage = damage;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        lifeTime -= Time.deltaTime;
        if (lifeTime < 0) Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage, other.transform.position);
            other.gameObject.GetComponent<Gwishin>().Velocity += (other.transform.position - transform.position).normalized * damage * 2;
            if (other.gameObject.GetComponent<EnemyHealth>().currentHealth < 0)
            {
                other.gameObject.GetComponent<EnemyHealth>().killedByMissle = true;
            }
        }

        Destroy(this.gameObject);
    }
}
