using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine : MonoBehaviour
{
    public List<State> states;

    public State initialState = null;

    State currentState;
    void Start()
    {
        currentState = initialState;   
    }

    // Update is called once per frame
    void Update()
    {
        Transition triggeredTransition = null;

        foreach(Transition transition in currentState.GetTransitions()){
            if(transition.IsTriggered()){
                triggeredTransition = transition;
                break;
            }
        }

        if(triggeredTransition!=null){
            State targetState = triggeredTransition.GetTargetState();
        

            currentState.DoExitAction();
            triggeredTransition.DoAction();
            targetState.DoEntryAction();

            currentState = targetState;
            return;

        }
        currentState.DoAction();

    }
}
