using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyState : State
{
    public override void DoAction(AIController ai){}
    public override void DoEntryAction(AIController ai){}
    public override void DoExitAction(AIController ai){}
}
