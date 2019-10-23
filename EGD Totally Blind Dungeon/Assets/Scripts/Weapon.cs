using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    Rigidbody rb;
    float velocity;
    public float minAngle;
    public float maxAngle;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity = rb.velocity.magnitude;
        if(gameObject.tag == "Enemy Weapon")
        {
            //Debug.Log(velocity);
        }
        //Debug.Log(velocity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.tag == "Enemy" && velocity > 5.0f)
        {
            // Do damage
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy Weapon" && velocity > 3.0f)
        {
            // Parry
            /*
            if (other.GetComponent<NPCController>().parryable == true)
            {
                // Do the parry
            }
            */

        }
        else if (other.tag == "Enemy Weapon" && velocity <= 3.0f)
        {
            // Block


        }
    }

}
