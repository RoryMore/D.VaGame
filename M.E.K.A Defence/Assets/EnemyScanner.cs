using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScanner : MonoBehaviour
{
    Missle parentScript;

    private void Awake()
    {
        parentScript = GetComponentInParent<Missle>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (parentScript.targetObject == null)
            {
                GetComponentInParent<Missle>().targetObject = other.gameObject;
            }
        }
    }
}
