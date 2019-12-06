using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrueTransition : Transition
{
    //this transition is always set to true, for one frame states

    public override bool IsTriggered(AIController ai){
        Debug.Log("true uwu");
        return true;
    }
    public override void DoAction(AIController ai){
    }
}
