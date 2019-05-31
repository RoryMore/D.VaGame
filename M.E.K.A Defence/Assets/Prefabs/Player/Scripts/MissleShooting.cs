using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleShooting : MonoBehaviour
{
    float missleDamage              = 60.0f;
    float missleVelocityMax         = 30.0f;
    float missleAccelerationRate    = 10.0f;
    float missleTurnSpeed           = 10.0f;

    float ammoCapacity              = 5.0f;
    float currentAmmo               = 5.0f;
    float fireRate                  = 0.2f;
    float fireRateTimer             = 0.0f;
    float ammoReplenishRate         = 2.0f;
    float ammoReplenishRateTimer    = 0.0f;

    Vector3 targetPosition;
    [SerializeField] GameObject misslePrefab;
    LayerMask hitMask;

    //Modifiers
    public float missileAmmoModifier;
    public float missileDamageModifier;
    public float missileReloadModifer;

    private void Awake()
    {
        hitMask = LayerMask.GetMask("Shootable");
    }

    private void Update()
    {
        ProcessTimers();
        ProcessInput();

        missileAmmoModifier = PlayerModifierManager.Instance.GetMissileAmmoCapacity();
        missileDamageModifier = PlayerModifierManager.Instance.GetMissileDamage();
        missileReloadModifer = PlayerModifierManager.Instance.GetMissileReplenishRate();
    }

    void ProcessInput()
    {
        if (Input.GetMouseButton(1))
        {
            if (CanFire())
            {
                GetTargetPosition();
                FireMissle();
            }   
        }
    }
    void ProcessTimers()
    {
        fireRateTimer += Time.deltaTime;
        ammoReplenishRateTimer += Time.deltaTime * -missileReloadModifer;

        if (ammoReplenishRateTimer > ammoReplenishRate && currentAmmo < ammoCapacity * missileAmmoModifier)
        {
            currentAmmo += 1;
            ammoReplenishRateTimer = 0;
        }
    }
    void GetTargetPosition()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, 1000, hitMask))
        {
            targetPosition = floorHit.point;
        }
        else
        {
            targetPosition = Vector3.forward * 100;
        }

        targetPosition.y = transform.position.y;
    }
    bool CanFire()
    {
        if (currentAmmo > 0 && fireRateTimer > fireRate)
        {
            return true;
        }
        return false;
    }
    void FireMissle()
    {
        GameObject missle = Instantiate(misslePrefab, transform.position, transform.rotation);
        Missle missleScript = missle.GetComponent<Missle>();
        missleScript.accelerationRate = missleAccelerationRate;
        missleScript.maxVelocity = missleVelocityMax;
        missleScript.turnSpeed = missleTurnSpeed;
        missleScript.damage = missleDamage * missileDamageModifier;
        missleScript.targetPosition = targetPosition;
        missleScript.heightTarget = targetPosition.y;

        fireRateTimer = 0;
        currentAmmo -= 1;
    }
}
