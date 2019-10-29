using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : State
{
    public EnemyAttack ea;
    public override void DoEntryAction(){
        ea.StartAttack();
    }
    public override void DoExitAction(){
    }

    public override void DoAction(){

    }

}
