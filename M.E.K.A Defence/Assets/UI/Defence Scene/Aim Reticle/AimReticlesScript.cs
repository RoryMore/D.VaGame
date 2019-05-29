using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum State
{
    ACTIVATING,
    ACTIVE,
    DEACTIVATING,
    UNACTIVE
}

public class AimReticlesScript : MonoBehaviour
{
    [SerializeField] Sprite reticleSprite;
    [SerializeField] float maxLength = 5;
    [SerializeField] float activatingAccelerationRate = 0.005f;
    [SerializeField] float SmoothRate = 0.0001f;
    [SerializeField] float numberReticles = 5;
    [SerializeField] float maxIdleTime = 2f;
    [SerializeField] GameObject attachedTo;
    [SerializeField] Vector3 attachedToOffset;
    [SerializeField] Color highlightColor = Color.red;

    Vector3 aimDirection;
    float aimLength;
    float activatingAcceleration = 0f;
    float percentActive = 0;
    float timeSinceInput = 0;
    

    Ray mouseRay;
    RaycastHit mouseHit;

    List<GameObject> reticles = new List<GameObject>();
    GameObject targetedEnemy = null;

    State currentState = State.UNACTIVE;


    private void Start()
    {
        CreateReticleParts();
    }

    private void Update()
    {
        transform.position = attachedTo.transform.position + attachedToOffset;
        CalculateTimeSinceInput();
        UpdateState();
        ProcessState();

        UpdateAimDirection();
        UpdateReticlePosition();
    }

    void DebugDrawing()
    {
        Debug.DrawLine(transform.position, transform.position + aimDirection * 100);
    }

    void UpdateAimDirection()
    {
        mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(mouseRay, out mouseHit, float.PositiveInfinity, LayerMask.GetMask("Shootable")))
        {
           
            aimDirection = Vector3.Normalize(mouseHit.point - transform.position);
            aimLength = Vector3.Distance(transform.position, mouseHit.point);
            if (mouseHit.transform.tag == "Enemy")
            {
                if (mouseHit.transform.gameObject != targetedEnemy && targetedEnemy)
                {
                    targetedEnemy.GetComponent<EnemyHealth>().Highlighted = false;
                }
                targetedEnemy = mouseHit.transform.gameObject;
                targetedEnemy.GetComponent<EnemyHealth>().Highlighted = true;
            }
            else if (targetedEnemy)
            {
                    targetedEnemy.GetComponent<EnemyHealth>().Highlighted = false;
                    targetedEnemy = null;
                
            }
        }
    }

    void CreateReticleParts()
    {
        for (int i = 0; i < numberReticles; i++)
        {
            reticles.Add(Instantiate(new GameObject("Reticle " + i), transform));
            
            SpriteRenderer sr = reticles[i].AddComponent<SpriteRenderer>();
            sr.sprite = reticleSprite;
        }
    }

    void UpdateReticlePosition()
    {
        float adjustedSmoothRate;
        float length = aimLength;
        if (length > maxLength * percentActive) length = maxLength * percentActive;

        reticles[0].transform.LookAt(transform.position);

        for (int i = 1; i < reticles.Count + 1; i++)
        {
            adjustedSmoothRate = Mathf.Clamp(SmoothRate * i, 0.1f, 0.9f);

            Vector3 smoothedDirection = Vector3.Lerp(transform.forward, aimDirection, ((float)i) / ((float)reticles.Count));
            Vector3 smoothedPosition = Vector3.Lerp(reticles[i - 1].transform.position, transform.position + smoothedDirection * length * ((float)i / (float)reticles.Count), adjustedSmoothRate);

            if(i == reticles.Count) Debug.DrawLine(transform.position, transform.position + smoothedDirection * 1000, Color.red);
            else Debug.DrawLine(transform.position, transform.position + smoothedDirection * 1000);

            reticles[i - 1].transform.position = smoothedPosition;
            reticles[i - 1].transform.rotation = reticles[0].transform.rotation;
            reticles[i - 1].transform.localScale = Vector3.one * (float)(numberReticles - i)/(float)numberReticles * Mathf.Clamp(percentActive * (reticles.Count - i), 0 , 1);
        }
    }

    void CalculateTimeSinceInput()
    {
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            timeSinceInput = 0;
        }
        else
        {
            timeSinceInput += Time.deltaTime;
        }
    }

    void UpdateState()
    {
        if (timeSinceInput <= 0)                currentState = State.ACTIVATING;
        else if (timeSinceInput > maxIdleTime) currentState = State.DEACTIVATING;
        

        switch (currentState)
        {
            case State.ACTIVATING:
                {
                    if (percentActive >= 1)
                    {
                        percentActive = 1;
                        currentState = State.ACTIVE;
                        activatingAcceleration = 0;
                    }
                    else
                    {
                        activatingAcceleration += activatingAccelerationRate;
                    }
                }
                break;
            case State.ACTIVE:
                {

                }
                break;
            case State.DEACTIVATING:
                {
                    if (percentActive <= 0)
                    {
                        percentActive = 0;
                        currentState = State.UNACTIVE;
                        activatingAcceleration = 0;
                    }
                    else
                    {
                        activatingAcceleration += activatingAccelerationRate;
                    }
                }
                break;
            case State.UNACTIVE:
                {
                    
                }
                break;
        }
    }

    void ProcessState()
    {
        switch (currentState)
        {
            case State.ACTIVATING:
                {
                    percentActive +=  activatingAcceleration;
                }
                break;
            case State.ACTIVE:
                {
                }
                break;
            case State.DEACTIVATING:
                {
                    percentActive -= activatingAcceleration;
                }
                break;
            case State.UNACTIVE:
                {
                }
                break;
        }
    }
}
