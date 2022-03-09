using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevel1 : MonoBehaviour
{

    [Header("Bullet Properties")]
    public GameObject bullet;
    public Transform bulletSpawnPosition;
    public ParticleSystem muzzleFlash;

    [Header("Explosion Properties")]
    public GameObject explosion;

    [Header("Behaviour Properties")]
    public float shootDelayFirstShoot = 2.5f;
    public float shootDelay = 5f;

    public GameObject Turret;

    private bool alive = true;
    private bool firstShoot = false;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(shoot(shootDelayFirstShoot, shootDelay));
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider collison) {

        if(collison.gameObject.tag == "Bullet")
        {
            Instantiate(explosion, transform.position, transform.rotation);
            //gameObject.GetComponent<TankExplosion>().explode();
            //gameObject.GetComponent<Animation>().Stop();
            Destroy(gameObject);
        }    
    }

    private IEnumerator shoot(float shootDelayFirstShoot, float shootDelay){
        while (alive)
        {
            if(firstShoot == false)
            {
                yield return new WaitForSeconds(shootDelayFirstShoot);
                Instantiate(bullet, bulletSpawnPosition.position, bulletSpawnPosition.rotation);
                muzzleFlash.Play();
                firstShoot = true;
            }
            else
            {
                yield return new WaitForSeconds(shootDelay);
                Instantiate(bullet, bulletSpawnPosition.position, bulletSpawnPosition.rotation);
                muzzleFlash.Play();
            }
            
        }        
    }
}
