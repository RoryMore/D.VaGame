using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolumetricLines;

public class LaserWeapon : Weapon
{
    VolumetricLineBehavior currentLaser;
    Vector3 targetPosition;
    RaycastHit laserHit;
    float aimSpeed = 0.4f;
    [SerializeField] Color startColour = Color.green;
    [SerializeField] Color focusedColour = Color.red;

    private void Update()
    {
        if (PlayerActivatesInput() && Stats.CurrentAmmo > 0)
        {
            FireLaser();
        }
        else if (PlayerDeactivatesInput() || Stats.CurrentAmmo <= 0)
        {
            currentLaser.GetComponent<Laser>().released = true;
            currentLaser = null;
        }

        UpdateLaser();
    }

    void FireLaser()
    {
        currentLaser = Instantiate(Stats.Bullet, transform.position, transform.rotation).GetComponent<VolumetricLineBehavior>();
        currentLaser.StartPos = transform.position;
        currentLaser.EndPos = transform.position;
        currentLaser.LineColor = startColour;
    }

    void UpdateLaser()
    {
        if (currentLaser == null) return;

        Stats.CurrentAmmo -= 1;

        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(mouseRay, out laserHit, Stats.Range, LayerMask.GetMask("Shootable")))
        {
            targetPosition = laserHit.point;
            if (laserHit.collider.tag == "Enemy" && CanFire)
            {
                laserHit.collider.GetComponent<EnemyHealth>().TakeDamage(Stats.BulletDamage, laserHit.point);
                CanFire = false;
            }
        }
        else targetPosition = transform.position + mouseRay.direction.normalized * Stats.Range;

        currentLaser.EndPos = Vector3.Lerp(currentLaser.EndPos, targetPosition, aimSpeed);

        
        if (Input.GetAxis("Mouse X") != 0 && Input.GetAxis("Mouse Y") != 0)
        {
            currentLaser.LineColor = Color.Lerp(currentLaser.LineColor, startColour, 0.1f);
        }
        else
        {
            currentLaser.LineColor = Color.Lerp(currentLaser.LineColor, focusedColour, 0.05f);
        }
    }
}
