using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailScript : MonoBehaviour
{
    public GameObject following;
    Vector3 poop = Vector3.zero;

    private void Update()
    {
        transform.position = poop;

    }
}
