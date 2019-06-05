using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolumetricLines;

public class Laser : MonoBehaviour
{
    VolumetricLineBehavior line;
    public bool released = false;

    private void Awake()
    {
        line = GetComponent<VolumetricLineBehavior>();
    }
    private void Update()
    {
        if (released)
        {
            line.StartPos = Vector3.Lerp(line.StartPos, line.EndPos, 0.4f);
            if (Vector3.Distance(line.EndPos, line.StartPos) < 1.0f)
            {
                Destroy(this.gameObject);
            }
        }

        
    }
}
