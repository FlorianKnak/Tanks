using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankExplosion : MonoBehaviour
{

    public GameObject Turret;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void explode()
    {
        Rigidbody rb = Turret.AddComponent(typeof(Rigidbody)) as Rigidbody;
        rb.AddForce(transform.up * 100);
    }

}
