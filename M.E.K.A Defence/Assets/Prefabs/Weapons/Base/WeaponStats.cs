using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Stats", menuName = "Weapon Stats")]
public class WeaponStats : ScriptableObject
{
    [Header("Ammunution")]

    [SerializeField]
    [Tooltip("The maximum ammo capacity of this weapon")]
    int ammoCapacityBase = 10;
    int ammoCapacity = 10;
    int currentAmmo = 0;


    [SerializeField]
    [Tooltip("The number of seconds it takes to replenish ammunition")]
    float replenishRateBase = 3f;
    float replenishRate = 3f;       //actual used stat

    [SerializeField]
    [Tooltip("how much ammo is replenished per tick of replenish rate")]
    int replenishAmount = 2;

    //----------//

    [Header("Rate of Fire")]

    [SerializeField]
    [Tooltip("How many seconds between shots")]
    float fireRate = 2f;

    [SerializeField]
    [Tooltip("How long the weapon must charge before firing")]
    float chargeTime = 0f;

    //----------//

    [Header("Range")]

    [SerializeField]
    [Tooltip("How far this weapon can fire")]
    float rangeBase = 30.0f;
    float range = 30.0f;

    [SerializeField]
    [Tooltip("How accurate this gun is")]
    float accurracy = 1.0f;

    //----------//

    [Header("Bullet")]

    [SerializeField]
    [Tooltip("The gameobject that will be created when the weapon is fired")]
    GameObject bullet = null;

    [SerializeField]
    [Tooltip("How much damage this weapon does per bullet")]
    float bulletDamageBase = 10f;
    float bulletDamage = 10f;

    [SerializeField]
    [Tooltip("The radius of the bullets damage effect (explosion radius)")]
    float bulletDamageRadius = 0f;

    [SerializeField]
    [Tooltip("How fast each bullet travels")]
    float bulletSpeed;


    // ----- Sound ----- //
    [Header("Sound")]

    [SerializeField]
    [Tooltip("The sounds this weapon will play")]
    WeaponSounds sounds;

    public float FireRate { get => fireRate; set => fireRate = value; }
    public float ReplenishRate { get => replenishRate; set => replenishRate = value; }
    public int ReplenishAmount { get => replenishAmount; set => replenishAmount = value; }
    public int AmmoCapacity { get => ammoCapacity; set => ammoCapacity = value; }
    public GameObject Bullet { get => bullet; set => bullet = value; }
    public float ChargeTime { get => chargeTime; set => chargeTime = value; }
    public float Range { get => range; set => range = value; }
    public int CurrentAmmo { get => currentAmmo; set => currentAmmo = value; }
    public float BulletSpeed { get => bulletSpeed; set => bulletSpeed = value; }
    public float BulletDamage { get => bulletDamage; set => bulletDamage = value; }
    public float ReplenishRateBase { get => replenishRateBase; set => replenishRateBase = value; }
    public int AmmoCapacityBase { get => ammoCapacityBase; set => ammoCapacityBase = value; }
    public float RangeBase { get => rangeBase; set => rangeBase = value; }
    public float BulletDamageBase { get => bulletDamageBase; set => bulletDamageBase = value; }
    public WeaponSounds Sounds { get => sounds; set => sounds = value; }
    public float BulletDamageRadius { get => bulletDamageRadius; set => bulletDamageRadius = value; }
    public float Accurracy { get => accurracy; set => accurracy = value; }

    private void Awake()
    {
        currentAmmo = AmmoCapacityBase;
        AmmoCapacity = AmmoCapacityBase;
    }
}
