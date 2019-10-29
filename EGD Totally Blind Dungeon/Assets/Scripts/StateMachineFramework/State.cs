using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public List<Transition> transitions;
    public abstract void DoAction();
    public abstract void DoEntryAction();
    public abstract void DoExitAction();

    public virtual List<Transition> GetTransitions(){return transitions;}
}
