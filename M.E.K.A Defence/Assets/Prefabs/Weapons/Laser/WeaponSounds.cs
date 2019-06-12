using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Sounds", menuName = "Weapon Sounds")]
public class WeaponSounds : ScriptableObject
{
    [SerializeField]
    [Tooltip("Plays before the weapon fires")]
    AudioClip chargingClip = null;

    public AudioClip ChargingClip { get => chargingClip; set => chargingClip = value; }
}
