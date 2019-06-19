using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReticle : MonoBehaviour
{
    RaycastHit laserHit;
    Vector3 targetPosition;
    GameObject player;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(mouseRay, out laserHit, 400, LayerMask.GetMask("Shootable")))
        {
            targetPosition = laserHit.point;
        }
        else targetPosition = mouseRay.origin + mouseRay.direction * 100;

        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.1f);
        transform.LookAt(Camera.main.transform.position);
    }
}
