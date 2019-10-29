using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyState2 : State
{
    public override void DoAction(){
        Debug.Log("Running Dummy State 2");
    }
    public override void DoEntryAction(){
        Debug.Log("Entering Dummy State 2");
    }
    public override void DoExitAction(){
        Debug.Log("Exitting Dummy State 2");
    }
}
