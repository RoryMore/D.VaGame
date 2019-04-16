using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletaltShoot : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bulletprefab;
    public Transform bulletSpawn;
    public float bulletspeed;
    public float lifetime = 3;
    float timer;
    float nextfire = 0.4f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.Space) && timer >= nextfire)
        {
            nextfire = timer + nextfire;
            Fire();
            nextfire = nextfire - timer;
            timer = 0.0f;
        }
        
    }

    private void Fire()
    {
        GameObject bullet = Instantiate(bulletprefab);
        Physics.IgnoreCollision(bullet.GetComponent<Collider>(), bulletSpawn.parent.GetComponent<Collider>());
        bullet.transform.position = bulletSpawn.position;
        Vector3 rotation = bullet.transform.rotation.eulerAngles;
        
        bullet.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);
        //add force
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward * bulletspeed, ForceMode.Impulse);
        StartCoroutine(BulletDead(bullet, lifetime));
    }

    private IEnumerator BulletDead(GameObject bullet, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(bullet);
    }
}
