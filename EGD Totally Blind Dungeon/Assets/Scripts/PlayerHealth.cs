using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 3;
    public float MAX_HEALTH = 3;
    public ItemTracker it;
    public Hand leftHand;
    public Hand rightHand;
    
    // Start is called before the first frame update
    void Start()
    {
        //it = gameObject.GetComponent<ItemTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        print("XXXXXXXXXXXXXXXXXXXX " + collision.gameObject.tag);
        if (collision.gameObject.tag == "Enemy Weapon")
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
        leftHand.DropRespawn();
        rightHand.DropRespawn();
        it.LoadPlayer();
        health = MAX_HEALTH;
    }
}
