using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour {

    public float health = 30;

    private float crashTime = 0;
    private bool startCrash = false;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        

        if (startCrash == true)
        {
            crashTime += Time.deltaTime;
        }

        if (crashTime > .7)
        {
            Destroy(gameObject);
        }

        KillAfterNoHealth();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name.Contains("Bullet"))
        {
            health -= 6;
        }
    }

    private void KillAfterNoHealth(){
        if(health <= 0)
        {
            startCrash = true;
            gameObject.GetComponent<ParticleSystem>().Play();
            gameObject.GetComponent<BoxCollider>().enabled = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
