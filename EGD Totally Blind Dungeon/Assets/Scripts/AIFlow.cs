using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFlow : MonoBehaviour
{
    public EnemyAttack verticalSwing;
    AIController ai;
    public float maxDistToPlayer = 20f;
    public float minDistToPlayer = 7f;
    bool isAttacking = false;
    bool walkingTowards = true;
    //switch the bools 2 transitions when we have a FSM later...
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        ai = GetComponent<AIController>();

    }

    // Update is called once per frame
    void Update()
    {
        DecideAction();
    }
    void DecideAction(){
        if(true) //i'll change 2 the damage shit later
        {
            if(walkingTowards){
                
            }
        }
    }
}
