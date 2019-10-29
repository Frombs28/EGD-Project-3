using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : State
{
    public EnemyAttack ea;
    public override void DoEntryAction(AIController ai){
        ai.currentAttack = ea;
        ea.StartAttack();
    }
    public override void DoExitAction(AIController ai){
    }

    public override void DoAction(AIController ai){

    }

}
