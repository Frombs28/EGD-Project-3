using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalSwingAttack : EnemyAttack
{
    public float angle = 45f;
    public float attackSpeed = 100f;
    public bool startOnBottom = false;
    int direction = 1;
    public bool startInFront = false;
    Vector3 originalOffset;
    Vector3 originalRotation;
    private void Start() {
        originalOffset = weapon.transform.localPosition;
        originalRotation = weapon.transform.eulerAngles;
    }
    public override void StartAttack(){
        attackCompleted = false;
        if(startOnBottom){
            direction = 1;
        }
        else{
            direction = -1;
        }
        weapon.transform.rotation = transform.rotation;
        weapon.transform.localPosition = originalOffset;
        //if(!startInFront) weapon.transform.RotateAround(transform.position, transform.right, angle*direction*-1);
        StartCoroutine("SwingSword");
    }
    public override void InterruptAttack(){
        StopAllCoroutines();
        attackCompleted = true;
    }

    IEnumerator SwingSword(){
        frameCount = 0;
        parryFrame = 4;
        parryable = false;
        attackCompleted = false;
        float currentAngle = 2*angle;
        while(currentAngle > 0){
            frameCount++;
//            Debug.Log(frameCount);
            if (frameCount >= parryFrame && !parryable)
            {
                parryable = true;
            }
            weapon.transform.RotateAround(transform.position, transform.forward * -1, attackSpeed*direction*Time.deltaTime);
            currentAngle-=attackSpeed*Time.deltaTime;
            yield return null;
        }
        parryable = false;
        attackCompleted = true;
        weapon.transform.localPosition = originalOffset;
        weapon.transform.eulerAngles = originalRotation;
    }

}
