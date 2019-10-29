using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyTransition1 : Transition
{
    public override void DoAction(){
        Debug.Log("Running Dummy Transition1");
    }
    public override bool IsTriggered(){
        return Input.GetKeyDown(KeyCode.Q);
    }
}
