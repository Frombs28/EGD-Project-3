using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardPlayerState : State
{
    public override void DoEntryAction(AIController ai){
    }
    public override void DoExitAction(AIController ai){
            ai.rb.velocity = Vector3.zero;
            ai.rb.angularVelocity = Vector3.zero;
    }

    public override void DoAction(AIController ai){
        Debug.Log("going toward player!!");
        ai.MoveTowardPlayer();
    }
}
