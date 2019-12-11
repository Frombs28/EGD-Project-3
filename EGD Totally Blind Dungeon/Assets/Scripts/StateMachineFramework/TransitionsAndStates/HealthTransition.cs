using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTransition : Transition
{
    public float healthSwitch;
    public override bool IsTriggered(AIController ai){
        Debug.Log("The current health is: " + ai.health);
        return ai.health<healthSwitch;
    }
    public override void DoAction(AIController ai){

    }
}
