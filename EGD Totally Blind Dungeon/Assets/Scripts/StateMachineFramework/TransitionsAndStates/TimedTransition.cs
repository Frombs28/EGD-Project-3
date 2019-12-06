using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedTransition : Transition
{
    public float timeToSwitch = 1f;
    float originalTime;
     /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
       originalTime = timeToSwitch; 
    }
     public override void DoAction(AIController ai){

     }
     public override bool IsTriggered(AIController ai){
         timeToSwitch-=Time.deltaTime;
         if(timeToSwitch<=0){
             timeToSwitch = originalTime;
             return true;
         }
         else{
             return false;
         }
     }
}
