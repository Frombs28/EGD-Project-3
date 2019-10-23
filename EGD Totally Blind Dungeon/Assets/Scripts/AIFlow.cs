using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFlow : MonoBehaviour
{
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
        player = GameObject.FindWithTag("Player");
        ai.verticalSwing.weapon.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        DecideAction();
    }
    void DecideAction(){
        if(true) //i'll change 2 the damage shit later
        {
            if(walkingTowards&&Vector3.Distance(player.transform.position, transform.position)>minDistToPlayer){
                //ai.rb.velocity = Vector3.zero;
                //ai.rb.angularVelocity = Vector3.zero;
                ai.MoveTowardPlayer();
                Debug.Log("Moving towards!");
            }
            else if(walkingTowards&&Vector3.Distance(player.transform.position, transform.position)<=minDistToPlayer){
                Debug.Log("starting attack!");
                ai.verticalSwing.weapon.SetActive(true);
                ai.verticalSwing.StartAttack();
                walkingTowards = false;
                ai.rb.velocity = Vector3.zero;
                ai.rb.angularVelocity = Vector3.zero;
                isAttacking = true;
            }
            else if(isAttacking&&!ai.verticalSwing.IsAttackDone()){
                Debug.Log("attacking!");
            }
            else if(isAttacking&&ai.verticalSwing.IsAttackDone()){
                Debug.Log("done attacking!");
                isAttacking = false;
                ai.verticalSwing.weapon.SetActive(false);
            }
            else if(!walkingTowards&&Vector3.Distance(player.transform.position, transform.position)<maxDistToPlayer){
                Debug.Log("Moving away!");
                ai.MoveAwayFromPlayer();
            }
            else{
                Debug.Log("starting 2 walk towards xd!");
                ai.rb.velocity = Vector3.zero;
                ai.rb.angularVelocity = Vector3.zero;
                walkingTowards = true;
            }
        }
    }
}
