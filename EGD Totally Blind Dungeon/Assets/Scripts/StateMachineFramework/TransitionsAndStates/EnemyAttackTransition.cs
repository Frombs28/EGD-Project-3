using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackTransition : Transition
{
    public EnemyAttack ea;
    public override bool IsTriggered(){
        return ea.IsAttackDone();
    }
    public override void DoAction(){
        
    }
}
