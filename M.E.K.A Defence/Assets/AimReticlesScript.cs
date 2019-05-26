using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimReticlesScript : MonoBehaviour
{
    [SerializeField] Sprite reticleSprite;
    [SerializeField] float maxLength = 10;
    [SerializeField] float SmoothRate = 0.8f;
    [SerializeField] float numberReticles;

    Vector3 AimDirection;
    float AimLength;

    Ray mouseRay;
    RaycastHit mouseHit;

    List<GameObject> reticles = new List<GameObject>();


    private void Start()
    {
        CreateReticleParts();
    }

    private void Update()
    {
        UpdateAimDirection();
        UpdateReticlePosition();
    }

    void UpdateAimDirection()
    {
        mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(mouseRay, out mouseHit))
        {
            AimDirection = Vector3.Normalize(mouseHit.point - Camera.main.transform.position);
            AimLength = Vector3.Distance(transform.position, mouseHit.point);
        }
    }

    void CreateReticleParts()
    {
        for (int i = 0; i < numberReticles; i++)
        {
            reticles.Add(Instantiate(new GameObject("Reticle " + i), transform));
            reticles[i].transform.localScale = Vector3.one * (numberReticles - i);
            SpriteRenderer sr = reticles[i].AddComponent<SpriteRenderer>();
            sr.sprite = reticleSprite;
        }
    }

    void UpdateReticlePosition()
    {
        float adjustedSmoothRate;
        float length = AimLength;
        if (length > maxLength) length = maxLength;
        


        for (int i = 1; i < reticles.Count + 1; i++)
        {
            adjustedSmoothRate = Mathf.Clamp(SmoothRate * i, 0.1f, 0.9f);

            Vector3 smoothedPosition = Vector3.Lerp(reticles[i - 1].transform.position, transform.position + AimDirection * length * (i / (float)reticles.Count), adjustedSmoothRate);

            reticles[i - 1].transform.position = smoothedPosition;
            reticles[i - 1].transform.LookAt(transform.position);
        }
    }
}
