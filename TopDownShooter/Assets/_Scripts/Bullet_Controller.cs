using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Controller : MonoBehaviour
{

    private float crashTime = 0;
    private bool startCrash = false;
    private Vector3 force = new Vector3(800, 0, 0);
    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(force);
    }

    // Update is called once per frame
    void Update()
    {
        ZToZero();

        if(startCrash == true)
        {
            crashTime += Time.deltaTime;
        }

        if(crashTime > .7)
        {
            Destroy(gameObject);
        }
    }

    // set player z to 0
    void ZToZero()
    {
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;
    }

    void OnCollisionEnter(Collision collision)
    {
        startCrash = true;
        gameObject.GetComponent<ParticleSystem>().Play();
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        

    }
}
