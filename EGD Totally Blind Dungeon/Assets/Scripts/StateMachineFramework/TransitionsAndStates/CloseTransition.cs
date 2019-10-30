using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseTransition : Transition
{
    public float minDistance = 2f;
    public override bool IsTriggered(AIController ai){
        return Vector3.Distance(ai.player.transform.position, ai.gameObject.transform.position)<minDistance;
    }
    public override void DoAction(AIController ai){

    }
}
