using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleWeapon : Weapon
{
    List<GameObject> targets = new List<GameObject>();
    bool aquiringTargets = false;

    private void Update()
    {
        if ((Input.GetKeyDown(UseKey) || Input.GetMouseButtonDown((int)UseButton)) && CanFire && !aquiringTargets)
        {
            StartCoroutine(ChargingWeapon(Stats.ChargeTime));
        }
        else if ((Input.GetKeyUp(UseKey) || Input.GetMouseButtonUp((int)UseButton)) && aquiringTargets)
        {
            StopCoroutine(ChargingWeapon(Stats.ChargeTime));
            aquiringTargets = false;
        }

        if (aquiringTargets)
        {
            //Get a number of enemies within range and add them to the targets container
        }
        else if (!aquiringTargets && targets.Count > 0)
        {
            FireMissles();
        }
    }

    void FireMissles()
    {
        foreach (GameObject target in targets)
        {
            Missle newMissle = Instantiate(Stats.Bullet, transform.position, transform.rotation).GetComponent<Missle>();
            newMissle.targetObject = target;
            targets.Remove(target);
        }
        CanFire = false;
    }

    IEnumerator ChargingWeapon(float chargeTime)
    {
        aquiringTargets = true;
        yield return new WaitForSeconds(chargeTime);
        aquiringTargets = false;
    }
}
