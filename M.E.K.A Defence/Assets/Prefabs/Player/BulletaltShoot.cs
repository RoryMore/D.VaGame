using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletaltShoot : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bulletprefab;
    public GameObject gun;
    public Transform bulletSpawn;
    public float bulletspeed;
    public float lifetime = 3;
    float timer;
    float nextfire = 0.4f;
    public ParticleSystem pS;
    Vector3 rotation;
    
    void Start()
    {
        rotation.x = -180.5f;
        rotation.y = -170.0f;
        rotation.z = 89.7f;
        
    }

    // Update is called once per frame
    void Update()
    {
       
        //    rotation.y = 180.0f - -(gun.transform.eulerAngles.y);
        
      
        //pS.transform.eulerAngles = rotation;
        //Vector3 gunpoint = gun.transform.position;
        //Vector3 upperbody = gun.transform.position;
        //gunpoint.y = 3.0f;
        ////if (upperbody.y >= 0)
        ////{
        ////    gunpoint.x -= 0.4f;
        ////}
        ////else
        ////{
        ////    gunpoint.x += 0.4f;
        ////}
        //pS.transform.position =gunpoint;
        
        timer += Time.deltaTime;
        if((Input.GetKeyDown(KeyCode.Space) && timer >= nextfire))
        {
            pS.Play();
            nextfire = timer + nextfire;
            Fire();
            nextfire = nextfire - timer;
            timer = 0.0f;
            
        }
        
    }

    private void Fire()
    {
        GameObject bullet = Instantiate(bulletprefab, bulletSpawn.position, Quaternion.identity , null);
        Physics.IgnoreCollision(bullet.GetComponent<Collider>(), bulletSpawn.parent.GetComponent<Collider>());
        //bullet.transform.position = bulletSpawn.position;
        //Vector3 rotation = bullet.transform.rotation.eulerAngles;
        
        bullet.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);
        //add force
        bullet.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * bulletspeed, ForceMode.Impulse);
        StartCoroutine(BulletDead(bullet, lifetime));
    }

    private IEnumerator BulletDead(GameObject bullet, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(bullet);
    }
}
