using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   Transform target;
    public float smoothing = 5f;
    public Vector3 offset = Vector3.zero;
    public Vector3 targetScreenPositionOffset = Vector3.zero;
    

    private void Awake()
    {
        target = GameObject.Find("Player").transform;
    }

    void FixedUpdate()
    {
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        transform.LookAt(target.position + targetScreenPositionOffset);
    }

}