using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : Weapon
{
    GameObject player;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < Stats.Range)
        {

        }
    }
}
