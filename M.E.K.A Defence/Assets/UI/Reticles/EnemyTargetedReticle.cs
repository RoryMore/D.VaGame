using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetedReticle : MonoBehaviour
{
    AudioSource source;
    GameObject following;
    AnimationClip clip;

    public GameObject Following { get => following; set => following = value; }

    // Start is called before the first frame update
    void Start()
    {
        // ----- Audio ----- //
        source = GetComponent<AudioSource>();
        if (source) source.Play();
    }

    private void Update()
    {
        if (following == null)
        {
            Destroy(this.gameObject);
            return;
        }
        Vector3 targetPosition = following.transform.position;
        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.8f);
    }
}
