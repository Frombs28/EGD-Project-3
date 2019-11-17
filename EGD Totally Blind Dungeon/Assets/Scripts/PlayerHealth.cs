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
    bool invincible = false;
    float timer;
    public float invincibleTime = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        //it = gameObject.GetComponent<ItemTracker>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (invincible)
        {
            timer += Time.deltaTime;
            if(timer >= invincibleTime)
            {
                invincible = false;
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        print("XXXXXXXXXXXXXXXXXXXX " + collision.gameObject.tag);
        if (collision.gameObject.tag == "Enemy Weapon")
        {
            AIController enemy = collision.gameObject.GetComponent<EnemyWeapon>().myEnemy.gameObject.GetComponent<AIController>();
            if(enemy.currentAttack != null && !enemy.currentAttack.attackCompleted && !invincible)
            {
                SubtractHealth(enemy.damage);
                invincible = true;
                timer = 0f;
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
