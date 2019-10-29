using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyTransition2 : Transition
{
    public override void DoAction(){
        Debug.Log("Running Dummy Transition2");
    }
    public override bool IsTriggered(){
        return Input.GetKeyDown(KeyCode.W);
    }
}
