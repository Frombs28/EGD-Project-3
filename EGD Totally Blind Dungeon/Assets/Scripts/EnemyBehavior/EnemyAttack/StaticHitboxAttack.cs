using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticHitboxAttack : EnemyAttack
{
    public List<BoxCollider> hitboxes;
    public float timeToAttack = 2f;
    public float attackActiveTime = 2f;
    void Start(){
       EnableHitboxes(false);
    }
    public override void StartAttack(){
        attackCompleted = false;
        StartCoroutine("SpawnHitbox");
    }
    public override void InterruptAttack(){
        StopAllCoroutines();
        EnableHitboxes(false);
        attackCompleted = true;
    }
    IEnumerator SpawnHitbox(){
        //maybe switch below to a while loop for sound timing
        yield return new WaitForSeconds(timeToAttack);
        EnableHitboxes(true);
        yield return new WaitForSeconds(attackActiveTime);
        EnableHitboxes(false);
        attackCompleted = true;
    }

    void EnableHitboxes(bool enable){
        foreach(var hitbox in hitboxes){
            hitbox.enabled = enable;
        }
    }
}
