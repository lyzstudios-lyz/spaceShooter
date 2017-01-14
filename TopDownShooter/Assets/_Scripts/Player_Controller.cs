using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour {

    // the speed multiplier for moving the player
    public float speed = 10.0f;

    

    public GameObject forwardThrust;
    public GameObject backThrust;
    public GameObject upThrust;
    public GameObject downThrust;

    public GameObject bullet;


    private float bulletTimeLimit = 1f;
    private float bulletTimeCurrent = 0;

    private Vector3 targetAngle = new Vector3(0f, 0f, 0f);
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        MovePlayer();
        ZToZero();

        FireWeapon();


    }


    // set player z to 0
    void ZToZero()
    {
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;
    }

    // Move the player - inlcude in Update
    private void MovePlayer()
    {
        // the force that will be applied to the player
        float forceH = (speed * Time.deltaTime);
        float forceV = (speed * Time.deltaTime);

        // set the 
        forceH = forceH * Input.GetAxis("Horizontal");
        forceV = forceV * Input.GetAxis("Vertical");
        // apply vertical force
        transform.GetComponent<Rigidbody>().AddForce(new Vector3(forceH, forceV, 0f));

        RotatePlayer();
        
    } 

    private void RotatePlayer()
    {
        float positive = 350f,
              negative = -350f;

        if (Input.GetAxis("Vertical") < 0)
        {
            targetAngle.x = positive;
            if (upThrust.GetComponent<ParticleSystem>().isStopped)
            {
                upThrust.GetComponent<ParticleSystem>().Play();
            }
        }
        else if (Input.GetAxis("Vertical") > 0)
        {
            targetAngle.x = negative;
            if (downThrust.GetComponent<ParticleSystem>().isStopped)
            {
                downThrust.GetComponent<ParticleSystem>().Play();
            }
        }


        if (Input.GetAxis("Horizontal") > 0)
        {
            targetAngle.z = positive;
            if (forwardThrust.GetComponent<ParticleSystem>().isStopped)
            {
                forwardThrust.GetComponent<ParticleSystem>().Play();
            }
            
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            targetAngle.z = negative;
            if (backThrust.GetComponent<ParticleSystem>().isStopped)
            {
                backThrust.GetComponent<ParticleSystem>().Play();
            }
        }

        
        if(Input.GetAxis("Horizontal") == 0)
        {
            targetAngle.z = 0f;
            forwardThrust.GetComponent<ParticleSystem>().Stop();
            backThrust.GetComponent<ParticleSystem>().Stop();
            
        }

        if (Input.GetAxis("Vertical") == 0)
        {
            targetAngle.x = 0f;
            upThrust.GetComponent<ParticleSystem>().Stop();
            downThrust.GetComponent<ParticleSystem>().Stop();
        }
        MovementRotation();
    }

    // rotate the player based on axis movement
    private void MovementRotation()
    {
        Vector3 currentAngle = transform.eulerAngles;

        currentAngle = new Vector3(
             Mathf.LerpAngle(currentAngle.x, targetAngle.x, Time.deltaTime*2),
             Mathf.LerpAngle(currentAngle.y, targetAngle.y, Time.deltaTime*2),
             Mathf.LerpAngle(currentAngle.z, targetAngle.z, Time.deltaTime*2)
        );

        transform.eulerAngles = currentAngle;
    }

    // fire the weapons gun
    private void FireWeapon()
    {
        Debug.Log(Input.GetAxis("Fire1"));
        if (Input.GetAxis("Fire1") > 0)
        {
            
            if(bulletTimeCurrent >= bulletTimeLimit)
            {
                Vector3 finalPos = transform.position;
                finalPos.x += .8f;
                bulletTimeCurrent = 0;
                Instantiate(bullet, finalPos, Quaternion.identity);
            }  
        }
        bulletTimeCurrent += Time.deltaTime;
    }
}


