using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportState : State
{
    bool hasTeleported = false;
    Teleport teleport;
    private void Start() {
        teleport = GetComponentInParent<Teleport>();
    }
    public override void DoAction(AIController ai){
        if(!hasTeleported){
            DoEntryAction(ai);
        }
    }
    public override void DoEntryAction(AIController ai)
    {
        //call teleport function here...
        Debug.Log("Gonna teleport uwu");
        teleport.Teleportation();
        hasTeleported = true;
    }
    public override void DoExitAction(AIController ai){
        hasTeleported = false;
    }
}
