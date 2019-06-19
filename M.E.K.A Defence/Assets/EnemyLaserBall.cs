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

    private void Awake()
    {
        line = GetComponent<VolumetricLineBehavior>();
        lineWidthBase = line.LineWidth;
        StartCoroutine(Death());
    }

    void Update()
    {
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

        if(other.tag != "Enemy") Destroy(this.gameObject);
    }
}
