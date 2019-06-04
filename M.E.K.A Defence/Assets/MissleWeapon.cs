using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleWeapon : Weapon
{
    List<GameObject> confirmedTargets = new List<GameObject>();
    bool firing = false;
    Scanner scanner;

    IEnumerator aquiringTargets;

    private void Awake()
    {
        scanner = GetComponentInChildren<Scanner>();
        scanner.SetupScanner("Enemy", Stats.Range);
        aquiringTargets = AquiringTargets(Stats.ChargeTime, Stats.FireRate);
    }

    private void Update()
    {
        print(scanner.ObjectsInRange.Count + " Objects");
        print(confirmedTargets.Count + " Targets");

        if (PlayerActivatesInput() && !firing)
        {
            //----- Start Adding Targets
            StartCoroutine(aquiringTargets);
        }
        else if (PlayerDeactivatesInput())
        {
            //----- Stop Adding Targets
            StopCoroutine(aquiringTargets);

            {
                //----- If there are any confirmed targets
                if (confirmedTargets.Count > 0)
                {
                    //----- Start Firing
                    StartCoroutine(Firing());
                }
            }
        }
    }

    IEnumerator Firing()
    {
        firing = true;

        //----- While there are still targets and we have ammo
        while (Stats.CurrentAmmo > 0 && confirmedTargets.Count > 0)
        {
            GameObject targetForNextMissle = confirmedTargets[0];

            //Create a missle at this position that's pointing upwards, then get the script that controls it.
            Missle nextMissle = Instantiate(Stats.Bullet, transform.position, Quaternion.LookRotation(Vector3.up)).GetComponent<Missle>();

            nextMissle.maxVelocity = Stats.BulletSpeed;
            nextMissle.damage = Stats.BulletDamage;
            nextMissle.targetObject = targetForNextMissle;
            targetForNextMissle.GetComponent<GwishinMovement>().TimesTargeted = 0;
            confirmedTargets.RemoveAt(0);

            Stats.CurrentAmmo--;
            print("Firing Missle");
            yield return new WaitForSeconds(0.125f);
        }
        firing = false;
    }

    IEnumerator AquiringTargets(float maxTime, float frequency)
    {
        float remainingTime = maxTime;

        while (remainingTime > 0 && confirmedTargets.Count < Stats.CurrentAmmo)
        {
            confirmedTargets.Add(GetTarget());
            yield return new WaitForSeconds(frequency);
            remainingTime -= frequency;
        }
    }

    bool PlayerActivatesInput()
    {
        //----- If the player presses the use key
        if (Input.GetKeyDown(UseKey))             return true;

        //----- If there is no mouse button
        if (UseButton == MouseButton.None)        return false;

        //----- if the player presses the mouse button
        if (Input.GetMouseButton((int)UseButton)) return true;

        //-----there is no input so return false
                                                  return false;
    }

    bool PlayerDeactivatesInput()
    {
        //----- If the player lets go of the use key
        if (Input.GetKeyUp(UseKey))                 return true;

        //----- If there isnt a mouse button 
        if (UseButton == MouseButton.None)          return false;

        //----- if the player lets go of the Mouse button
        if (Input.GetMouseButtonUp((int)UseButton)) return true;

        //-----there is no input so return false
                                                    return false;
    }

    GameObject GetTarget()
    {
        int maxTargetedAmount = 1;
        List<GameObject> possibleTargets = scanner.ObjectsInRange;
        possibleTargets.Sort(SortByDistance);

        while (true)
        {
            for (int i = possibleTargets.Count - 1; i >= 0; i--)
            {
                GwishinMovement targetsScript = possibleTargets[i].GetComponent<GwishinMovement>();
                if (targetsScript.TimesTargeted < maxTargetedAmount)
                {
                    targetsScript.AddTargetedReticle();
                    targetsScript.TimesTargeted++;
                    return possibleTargets[i];
                }
            }
            maxTargetedAmount++;
        }
    }

    static int SortByDistance(GameObject targetA, GameObject targetB)
    {
        return targetA.GetComponent<GwishinMovement>().DistanceFromPlayer.CompareTo(targetB.GetComponent<GwishinMovement>().DistanceFromPlayer);
    }
}
