using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    public State targetState;
    
    public abstract void DoAction(AIController ai);
    public virtual State GetTargetState(){ return targetState;}
    public abstract bool IsTriggered(AIController ai);
}
