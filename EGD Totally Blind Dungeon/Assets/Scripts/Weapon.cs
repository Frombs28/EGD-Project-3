using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    Rigidbody rb;
    Interact interact;
    float velocity;
    public float minAngle;
    public float maxAngle;
    public float damage;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        interact = GetComponent<Interact>();
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
            other.GetComponent<AIController>().SubtractHealth(damage);
        }
        //else print(collision.gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        //print(velocity);
        if (other.tag == "Enemy Weapon" && velocity > 8.0f)
        {
            // Parry
            GameObject enemy = other.gameObject.GetComponent<EnemyWeapon>().myEnemy;
            if (enemy.GetComponent<AIController>().IsParryable())
            {
                enemy.GetComponent<AIController>().Stun();
                Debug.Log("Parry the platypus?!");
            }
            

        }
        else if (other.tag == "Enemy Weapon" && velocity <= 3.0f)
        {
            // Block


        }
    }

}
