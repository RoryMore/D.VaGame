using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Sounds", menuName = "Weapon Sounds")]
public class WeaponSounds : ScriptableObject
{
    [SerializeField]
    [Tooltip("Plays before the weapon fires")]
    AudioClip chargingClip = null;

    [SerializeField]
    [Tooltip("Plays when Weapon Gains Ammo")]
    AudioClip ammoIncrease = null;

    [SerializeField]
    [Tooltip("Plays when a bullet is created")]
    AudioClip bulletFire = null;

    public AudioClip ChargingClip { get => chargingClip; set => chargingClip = value; }
    public AudioClip AmmoIncrease { get => ammoIncrease; set => ammoIncrease = value; }
    public AudioClip BulletFire { get => bulletFire; set => bulletFire = value; }
}
