using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    GameObject target;
    public float smoothing = 5f;
    public Vector3 offset = Vector3.zero;
    public Vector3 targetScreenPositionOffset = Vector3.zero;
    Vector3 mouseOffset = Vector3.zero;
    

    private void Awake()
    {
        target = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        mouseOffset = Vector3.Lerp(mouseOffset, -mouseRay.direction * 20, 0.4f);


        Vector3 targetCamPos = target.transform.position + offset + mouseOffset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        

        transform.LookAt(target.transform.position + targetScreenPositionOffset);
    }

}