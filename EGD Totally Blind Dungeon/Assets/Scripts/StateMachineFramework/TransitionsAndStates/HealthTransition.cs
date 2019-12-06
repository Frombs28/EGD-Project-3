using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTransition : Transition
{
    public float healthSwitch;
    public override bool IsTriggered(AIController ai){
        return ai.health<healthSwitch;
    }
    public override void DoAction(AIController ai){

    }
}
