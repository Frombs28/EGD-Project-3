using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 3;
    public ItemTracker it;
    
    // Start is called before the first frame update
    void Start()
    {
        it = gameObject.GetComponent<ItemTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy Weapon")
        {
            AIController enemy = collision.gameObject.GetComponent<EnemyWeapon>().myEnemy.gameObject.GetComponent<AIController>();
            if(enemy.currentAttack != null && !enemy.currentAttack.attackCompleted)
            {
                SubtractHealth(enemy.damage);
            }
        }
    }

    public void SubtractHealth(float damage)
    {
        health -= damage;

        print("I've been injured! Ugh!");

        // PLAY INJURED AUDIO

        if (health <= 0)
        {
            // DEATH: PLAY DIE AUDIO
            Death();
        }
    }

    public void Death()
    {
        it.LoadPlayer();
    }
}
