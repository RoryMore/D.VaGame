using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBullet : MonoBehaviour
{
    float speed;
    float damage;
    float lifeTime = 10;
    [SerializeField] GameObject hitParticle = null;

    public void SetupBullet(float speed, float damage)
    {
        this.speed = speed;
        this.damage = damage;
    }

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
            other.gameObject.GetComponent<Gwishin>().Velocity += (transform.position - other.transform.position).normalized * damage;
        }

        if (other.gameObject.tag != "Player")
        {
            print(other.gameObject.name);
            Instantiate(hitParticle, transform.position, Random.rotation);
            Destroy(this.gameObject);
        }

        
    }
}
