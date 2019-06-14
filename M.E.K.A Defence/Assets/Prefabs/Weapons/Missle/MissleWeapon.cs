using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissleWeapon : Weapon
{
    List<GameObject> confirmedTargets = new List<GameObject>();
    [SerializeField] Sprite reticle;

    Scanner scanner;

    IEnumerator aquiringTargets;

    private void Awake()
    {
        scanner = GetComponentInChildren<Scanner>();
        scanner.SetupScanner("Enemy", Stats.Range);
        //this.Stats = PlayerModifierManager.Instance.MissleWeaponStats;
    }

    private void Update()
    {
        print(PlayerActivatesInput());
        if (PlayerActivatesInput()  && !Firing && !Charging && !Aquiring)
        {
            //----- Start Adding Targets
            Aquiring = true;
            aquiringTargets = AquiringTargets(Stats.FireRate);
            StartCoroutine(aquiringTargets);
        }
        else if (PlayerDeactivatesInput())
        {
            //----- Stop Adding Targets
            Aquiring = false;
            StopCoroutine(aquiringTargets);

            {
                //----- If there are any confirmed targets
                if (confirmedTargets.Count > 0)
                {
                    //----- Start Firing
                    StartCoroutine(FiringMissles());
                }
            }
        }
    }

    IEnumerator FiringMissles()
    {
        Firing = true;
        StartCoroutine(WeaponCharge(Stats.ChargeTime));

        while (Charging)
        {
            yield return new WaitForEndOfFrame();
        }

        //----- While there are still targets and we have ammo
        while (confirmedTargets.Count > 0)
        {
            GameObject targetForNextMissle = confirmedTargets[0];

            //Create a missle at this position that's pointing upwards, then get the script that controls it.
            Missle nextMissle = Instantiate(Stats.Bullet, transform.position, Quaternion.LookRotation(Vector3.up + transform.forward)).GetComponent<Missle>();

            nextMissle.MaxVelocity = Stats.BulletSpeed;
            nextMissle.Damage = Stats.BulletDamage;
            nextMissle.targetObject = targetForNextMissle;
            nextMissle.DamageRadius = Stats.BulletDamageRadius;
            //targetForNextMissle.GetComponent<GwishinMovement>().TimesTargeted = 0;
            confirmedTargets.RemoveAt(0);

            
            print(confirmedTargets.Count);
            yield return new WaitForSeconds(0.125f);
        }
        Firing = false;
        print("Firing Complete");
    }

    IEnumerator AquiringTargets(float frequency)
    {
        while (Stats.CurrentAmmo > 0 && scanner.ObjectsInRange.Count > 0)
        {
            confirmedTargets.Add(GetTarget());
            ReduceCurrentAmmo(1);
            yield return new WaitForSeconds(Random.Range(frequency * 0.5f, frequency));
        }
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
