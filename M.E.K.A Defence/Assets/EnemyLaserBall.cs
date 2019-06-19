using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolumetricLines;

public class EnemyLaserBall : MonoBehaviour
{
    float speed = 25f;
    float damage = 10f;
    VolumetricLineBehavior line;
    float lineWidthBase = 50f;

    [SerializeField] AudioClip fireClip = null;
    [SerializeField] AudioClip collideClip = null;
    AudioSource source = null;
    bool dead = false;

    private void Awake()
    {
        line = GetComponent<VolumetricLineBehavior>();
        lineWidthBase = line.LineWidth;
        StartCoroutine(Death());

        source = GetComponent<AudioSource>();
        source.clip = fireClip;
        source.Play();

    }

    void Update()
    {
        if (dead)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, 0.1f);
            if(!source.isPlaying) Destroy(this.gameObject);
            return;
        }

        line.LineWidth = lineWidthBase + (lineWidthBase/10f) * Mathf.Sin(Time.time  * 5);
        transform.position += transform.forward * speed * Time.deltaTime;
    }
    public void Setup(float speed, float damage)
    {
        this.speed = speed;
        this.damage = damage;
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(15);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
        }

        if (other.tag != "Enemy")
        {
            source.clip = collideClip;
            source.volume = 0.1f;
            source.Play();
            dead = true;
            GetComponent<VolumetricLineBehavior>().LineColor = Color.red;
            Destroy(GetComponent<Collider>());
        }
    }
}
