using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStorage : MonoBehaviour
{

    [SerializeField] WeaponStats missleWeaponStats = null;
    [SerializeField] WeaponStats laserWeaponStats = null;

    public WeaponStats MissleWeaponStats { get => missleWeaponStats; set => missleWeaponStats = value; }
    public WeaponStats LaserWeaponStats { get => laserWeaponStats; set => laserWeaponStats = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        PlayerModifierManager.Instance.LaserWeaponStats = this.LaserWeaponStats;
        PlayerModifierManager.Instance.MissleWeaponStats = this.MissleWeaponStats;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
