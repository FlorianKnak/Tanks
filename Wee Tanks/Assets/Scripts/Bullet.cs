using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Properties")]
    public float bulletSpeed;
    public float bulletLifetime;
    public int bulletBounces = 1;
    public Renderer bulletRenderer;

    [Header("Particle System")]
    public ParticleSystem trail;



    // Start is called before the first frame update
    void Start()
    {
       Destroy(gameObject, bulletLifetime);
    }

    void Update()
    {
      
        transform.Translate(Vector3.forward * Time.deltaTime * bulletSpeed);

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Time.deltaTime * bulletSpeed + .1f) && bulletBounces > 0){
            Vector3 reflectDir = Vector3.Reflect(ray.direction, hit.normal);
            float rot = 90 - Mathf.Atan2(reflectDir.z, reflectDir.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, rot, 0);
            bulletBounces--;
        }

    }

    private void OnTriggerEnter(Collider other) {

        if(bulletBounces == 0)
        {
            destroyBullet();
        }

        if(other.gameObject.tag == "Bullet")
        {
            destroyBullet();
        }

        if(other.gameObject.tag == "Enemy")
        {
            destroyBullet();
        }

    }

    private void destroyBullet() {       
        bulletRenderer = GetComponent<Renderer>();
        bulletRenderer.enabled = false;
        Collider collider = GetComponent<Collider>();
        collider.enabled = false;
        var emission = trail.emission;
        emission.rateOverTime = 0f;
        Destroy(gameObject, 5f);      
    }

}
