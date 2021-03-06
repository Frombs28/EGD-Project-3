﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public List<SimpleObserver> observers;
    public List<Transition> transitions;
    public abstract void DoAction(AIController ai);
    public abstract void DoEntryAction(AIController ai);
    public abstract void DoExitAction(AIController ai);

    public virtual void NotifyObservers(NotificationType notice, string message, GameObject go){
        foreach(SimpleObserver obs in observers){
            obs.OnNotify(notice, message, go);
        }
    }

    public virtual List<Transition> GetTransitions(){return transitions;}
}
