using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FFBSecondPhaseState : State
{
    FirstFloorBossManager manager; 
    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponentInParent<FirstFloorBossManager>();
    }

    public override void DoAction(AIController ai){}
    public override void DoEntryAction(AIController ai){
        manager.StartSecondPhase();
    }
    public override void DoExitAction(AIController ai){}
}
