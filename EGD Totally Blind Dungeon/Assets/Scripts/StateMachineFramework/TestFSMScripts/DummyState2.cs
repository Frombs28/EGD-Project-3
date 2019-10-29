using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyState2 : State
{
    public override void DoAction(AIController ai){
        Debug.Log("Running Dummy State 2");
    }
    public override void DoEntryAction(AIController ai){
        Debug.Log("Entering Dummy State 2");
    }
    public override void DoExitAction(AIController ai){
        Debug.Log("Exitting Dummy State 2");
    }
}
