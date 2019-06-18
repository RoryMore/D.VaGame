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
    }

    void Update()
    {
        line.LineWidth = lineWidthBase + (lineWidthBase/10f) * Mathf.Sin(Time.time  * 5);
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
