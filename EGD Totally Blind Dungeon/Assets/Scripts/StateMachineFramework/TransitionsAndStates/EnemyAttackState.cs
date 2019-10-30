using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : State
{
    public EnemyAttack ea;
    bool isAttacking = false;
    public override void DoEntryAction(AIController ai){
        isAttacking = true;
        ai.currentAttack = ea;
        ea.StartAttack();
    }
    public override void DoExitAction(AIController ai){
        isAttacking = false;
    }

    public override void DoAction(AIController ai){
        if(!isAttacking){
            ai.currentAttack = ea;
            ea.StartAttack();
            isAttacking = true;
        }

    }

}
