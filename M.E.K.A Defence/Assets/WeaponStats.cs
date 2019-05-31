using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Stats", menuName = "Weapon Stats")]
public class WeaponStats : ScriptableObject
{
    [Header("Ammunution")]

    [SerializeField]
    [Tooltip("The maximum ammo capacity of this weapon")]
    int ammoCpacity = 10;

    [SerializeField]
    [Tooltip("The number of seconds it takes to replenish ammunition")]
    float replenishRate = 3f;

    [SerializeField]
    [Tooltip("how much ammo is replenished per tick of replenish rate")]
    int replenishAmount = 2;

    //----------//

    [Header("Damage")]

    [SerializeField]
    [Tooltip("How much damage this weapon does per bullet")]
    float damage = 10f;

    //----------//

    [Header("Rate of Fire")]

    [SerializeField]
    [Tooltip("How many seconds between shots")]
    float fireRate = 2f;

    [SerializeField]
    [Tooltip("Can the gun be fired by holding down the button")]
    bool holdToFire;

    [SerializeField]
    [Tooltip("How long the weapon must charge before firing")]
    float chargeTime = 0f;

    //----------//

    [Header("Bullet")]
    [SerializeField]
    [Tooltip("The gameobject that will be created when the weapon is fired")]
    GameObject bullet = null;

    public float FireRate { get => fireRate; set => fireRate = value; }
    public float ReplenishRate { get => replenishRate; set => replenishRate = value; }
    public int ReplenishAmount { get => replenishAmount; set => replenishAmount = value; }
    public int AmmoCapacity { get => ammoCpacity; set => ammoCpacity = value; }
    public GameObject Bullet { get => bullet; set => bullet = value; }
    public float ChargeTime { get => chargeTime; set => chargeTime = value; }
}
