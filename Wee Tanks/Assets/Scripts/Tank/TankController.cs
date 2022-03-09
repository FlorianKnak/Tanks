using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{

    [Header("Bullet Properties")]
    public GameObject bullet;
    public Transform bulletSpawnPosition;
    public ParticleSystem muzzleFlash;
    public float bulletSpeed;
    public float bulletLifetime;
    public float fireRate;
    private float nextFire = 0f;

    [Header("Mine Properties")]
    public GameObject mine;
    public Transform mineSpawnPosition;
    public int mineAmount = 3;
    public float mineRate = 1f;
    private float nextMine = 0f;
    public float detonationTime = 10f;
    public bool explodeAfterTime = true;

    [Header("Movement Properties")]
    public float rotateSpeed = 90;
   	public float speed = 5f;

    [Header("Turret Properties")]
    public new Camera camera;
    public Transform turretTransform;
    Vector3 reticlePosition;
    Vector3 reticleNormal;

    [Header("Effects")]
    public GameObject explosion;


    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {

        getReticlePosition();
        moveTurret();

         //Check if Shoot Button is clicked
        if(Input.GetButtonDown("Fire1") && Time.time > nextFire){
            nextFire = Time.time + fireRate;
            Instantiate(bullet, bulletSpawnPosition.position, bulletSpawnPosition.rotation);
            gameObject.GetComponent<AudioSource>().Play();
            muzzleFlash.Play();
        }


        var transAmount = speed * Time.deltaTime;
        var rotateAmount = rotateSpeed * Time.deltaTime;

        if (Input.GetKey("w"))
        {
            transform.Translate(-transAmount, 0, 0);
        }
        if (Input.GetKey("s"))
        {
            transform.Translate(transAmount, 0, 0);
        }
        if (Input.GetKey("a"))
        {
            transform.Rotate(0, -rotateAmount, 0);
        }
        if (Input.GetKey("d"))
        {
            transform.Rotate(0, rotateAmount, 0);
        }

        if(Input.GetButtonDown("Jump") && explodeAfterTime == true && Time.time > nextMine)
        {
            if(mineAmount > 0){
            nextMine = Time.time + mineRate;
            Instantiate(mine, mineSpawnPosition.position, mineSpawnPosition.rotation);
            mineAmount--;
            }else{
            Debug.Log("No mines left");
            }
        }


    }

    private void OnTriggerEnter(Collider collison) {

        if(collison.gameObject.tag == "Bullet")
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);           
        }    
    }

    private void getReticlePosition()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            reticlePosition = hit.point;
            reticleNormal = hit.normal;
        }

    }

    private void moveTurret()
    {
        Vector3 turretLookDir = reticlePosition - turretTransform.position;
        turretLookDir.y = 0f;
        turretTransform.rotation = Quaternion.LookRotation(turretLookDir);
    }

}
