using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackTransition : Transition
{
    public override bool IsTriggered(AIController ai){
        return ai.currentAttack==null||ai.currentAttack.IsAttackDone();
    }
    public override void DoAction(AIController ai){

    }
}
