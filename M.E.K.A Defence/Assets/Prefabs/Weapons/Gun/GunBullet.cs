using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBullet : MonoBehaviour
{
    float speed;
    float damage;
    float lifeTime = 10;
    [SerializeField] GameObject hitParticle = null;

    [SerializeField] AudioClip fireClip = null;
    [SerializeField] AudioClip collideClip = null;
    AudioSource source;
    bool dead = false;

    public void SetupBullet(float speed, float damage)
    {
        this.speed = speed;
        this.damage = damage;
    }

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        source.clip = fireClip;
        source.volume = 0.1f;
        source.Play();
    }
    void Update()
    {
        if(!dead) transform.position += transform.forward * speed * Time.deltaTime;

        lifeTime -= Time.deltaTime;
        if (lifeTime < 0) Destroy(this.gameObject);

        if (dead && !source.isPlaying) Destroy(this.gameObject);
        
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
            source.clip = collideClip;
            source.volume = 0.05f;
            source.Play();
            dead = true;
            Destroy(GetComponent<Collider>());
        }

        
    }
}
