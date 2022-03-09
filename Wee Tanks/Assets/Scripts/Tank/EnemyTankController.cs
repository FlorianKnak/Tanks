using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankController : MonoBehaviour
{

    public Transform playerTankTransform;
    public Transform turretTransform;
    public Transform bulletSpawnPosition;
    public GameObject explosion;

    private bool alive = true;

    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine(shoot());
    }

    // Update is called once per frame
    void Update()
    {
        moveTurret();

        RaycastHit hit;

        if (Physics.Raycast(turretTransform.position, transform.forward, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(turretTransform.position, transform.forward * hit.distance, Color.yellow);
        }                 

    }

    private void OnTriggerEnter(Collider collison) {

        if(collison.gameObject.tag == "Bullet")
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }    
    }

    private IEnumerator shoot(){
        while (alive)
        {
            yield return new WaitForSeconds(Random.Range(2, 5));
            Instantiate(bullet, bulletSpawnPosition.position, bulletSpawnPosition.rotation);
        }        
    }

    private void moveTurret()
    {
        Vector3 turretLookDir = playerTankTransform.position - turretTransform.position;
        turretLookDir.y = 0f;
        turretTransform.rotation = Quaternion.LookRotation(turretLookDir);
    }

}
