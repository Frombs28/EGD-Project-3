using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSwingAttack : EnemyAttack
{
    public float angle = 45f;
    public float attackSpeed = 100f;
    public bool isClockwise = false;
    int direction = 1;
    public bool startInFront = false;
    public override void StartAttack(){
        attackCompleted = false;
        if(isClockwise){
            direction = 1;
        }
        else{
            direction = -1;
        }
        weapon.transform.rotation = transform.rotation;
        if(!startInFront) weapon.transform.RotateAround(transform.position, transform.up, angle*direction*-1);
        StartCoroutine("SwingSword");
    }
    public override void InterruptAttack(){
        StopAllCoroutines();
        attackCompleted = true;
    }

    IEnumerator SwingSword(){
        attackCompleted = true;
        float currentAngle = 2*angle;
        while(currentAngle > 0){
            weapon.transform.RotateAround(transform.position, transform.up, attackSpeed*direction*Time.deltaTime);
            currentAngle-=attackSpeed*Time.deltaTime;
            yield return null;
        }
        attackCompleted = true;
    }
}
