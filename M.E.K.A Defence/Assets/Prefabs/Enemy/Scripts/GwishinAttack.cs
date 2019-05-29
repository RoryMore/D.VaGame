using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GwishinAttack : MonoBehaviour
{
    float bulletDamage = 10.0f;
    float bulletSpeed = 10.0f;
    float range = 50.0f;
    float FireChance = 0.01f;
    GameObject player;
    [SerializeField] GameObject bullet;

    float bulletSpeedModifier = 1.0f;
    float bulletDamageModifier = 1.0f;
    float fireDelayModifier = 1.0f;


    

    //timer
    float fireDelay = 10.0f;
    float fireTimer = 0.0f;

    private void Awake()
    {
        player = GameObject.Find("Player");

        bulletDamageModifier =  (1 + (PlayerModifierManager.Instance.GetWaveCount() / 10));
        bulletSpeedModifier = (1 +(PlayerModifierManager.Instance.GetWaveCount() / 10));
        fireDelayModifier = (1 + (PlayerModifierManager.Instance.GetWaveCount() / 10));
    }

    private void Update()
    {
        ProcessTimer();
        if (CanFire())
        {
            Fire();
        }
    }

    void ProcessTimer()
    {
        fireTimer += Time.deltaTime * fireDelayModifier;
    }
    bool CanFire()
    {
        if (fireTimer > fireDelay && Random.value < FireChance && Vector3.Distance(player.transform.position, transform.position) < range)
        {
            return true;
        }
        else return false;
    }
    void Fire()
    {
        fireTimer = 0.0f;
        CreateBullet();
    }
    void CreateBullet()
    {
        EnemyBullet newBulletScript = Instantiate(bullet, transform.position, transform.rotation).GetComponent<EnemyBullet>();
        newBulletScript.speed = GetComponent<GwishinMovement>().maxVelocity * bulletSpeedModifier;
        newBulletScript.damage = bulletDamage * bulletDamageModifier;

        //TrailScript newTrail = Instantiate(trail, newBulletScript.gameObject.transform.position, newBulletScript.gameObject.transform.rotation).GetComponent<TrailScript>();
        //newTrail.following = newBulletScript.gameObject;
    }
}
