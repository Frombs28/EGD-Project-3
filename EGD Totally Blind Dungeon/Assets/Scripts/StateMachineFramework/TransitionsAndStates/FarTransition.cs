using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarTransition : Transition
{
    public float maxDistance = 7f;
    public float timer = 1f;
    Vector3 prev;

    public override bool IsTriggered(AIController ai){
        //Debug.Log(timer);
        if (timer <= 0){
            return true;
        }
        //Debug.Log("Dist " + Vector3.Distance(prev, ai.gameObject.transform.position));
        if (Vector3.Distance(prev, ai.gameObject.transform.position) < .001f){
            timer -= Time.deltaTime;
            //prev = transform.position;
        }
        prev = ai.gameObject.transform.position;

        return Vector3.Distance(ai.player.transform.position, ai.gameObject.transform.position)>maxDistance;
    }
    public override void DoAction(AIController ai){
        prev = ai.gameObject.transform.position;
        timer = 1f;
    }
}
