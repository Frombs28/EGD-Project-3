using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardPlayerState : State
{
    public override void DoEntryAction(AIController ai){
    }
    public override void DoExitAction(AIController ai){
    }

    public override void DoAction(AIController ai){
        ai.MoveTowardPlayer();
    }
}
