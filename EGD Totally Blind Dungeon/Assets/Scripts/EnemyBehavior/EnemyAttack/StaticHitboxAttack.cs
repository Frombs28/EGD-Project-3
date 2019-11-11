using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticHitboxAttack : EnemyAttack
{
    public BoxCollider hitbox;
    public float timeToAttack = 2f;
    public float attackActiveTime = 2f;
    void Start(){
        hitbox.enabled = false;
    }
    public override void StartAttack(){
        attackCompleted = false;
        StartCoroutine("SpawnHitbox");
    }
    public override void InterruptAttack(){
        StopAllCoroutines();
        hitbox.enabled = false;
        attackCompleted = true;
    }
    IEnumerator SpawnHitbox(){
        //maybe switch below to a while loop for sound timing
        yield return new WaitForSeconds(timeToAttack);
        hitbox.enabled = true;
        yield return new WaitForSeconds(attackActiveTime);
        hitbox.enabled = false;
        attackCompleted = true;
    }
}
