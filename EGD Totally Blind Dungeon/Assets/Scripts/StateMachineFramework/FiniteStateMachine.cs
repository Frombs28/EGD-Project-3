using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine : MonoBehaviour
{
    public List<State> states;

    public AIController ai = null;

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
            if(transition.IsTriggered(ai)){
                triggeredTransition = transition;
                break;
            }
        }

        if(triggeredTransition!=null){
            State targetState = triggeredTransition.GetTargetState();
        

            currentState.DoExitAction(ai);
            triggeredTransition.DoAction(ai);
            targetState.DoEntryAction(ai);

            currentState = targetState;
            return;

        }
        currentState.DoAction(ai);

    }
}
