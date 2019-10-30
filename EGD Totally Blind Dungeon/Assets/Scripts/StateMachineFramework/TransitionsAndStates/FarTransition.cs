using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarTransition : Transition
{
    public float maxDistance = 7f;
    public override bool IsTriggered(AIController ai){
        return Vector3.Distance(ai.player.transform.position, ai.gameObject.transform.position)>maxDistance;
    }
    public override void DoAction(AIController ai){

    }
}
