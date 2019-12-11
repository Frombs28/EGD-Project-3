using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstFloorBossManager : SimpleObserver
{
    List<AIController> activeBosses;
    Vector3 initialPosition;
    public Transform duplicatePosition = null;
    FiniteStateMachine finiteStateMachine;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        initialPosition = transform.parent.position;
        AIController main = transform.parent.GetComponent<AIController>();
        activeBosses = new List<AIController>();
        activeBosses.Add(main);
        if(duplicatePosition!=null) GetComponentInChildren<SpawnState>().spawnLocation = duplicatePosition.position;
        else GetComponentInChildren<SpawnState>().spawnLocation = Vector3.zero;
        finiteStateMachine = GetComponentInChildren<FiniteStateMachine>();
        StopFSM();
    }
    private void Update() {
        UpdateSharedHealth();
        if(Input.GetKeyDown(KeyCode.W)){
            Debug.Log("trying 2 reset");
            ResetBoss();
        }
    }
    public override void OnNotify(NotificationType notice, string message, GameObject go){
        if(notice == NotificationType.SpawnedItem){
            Debug.Log("The chasey fella should be spawned");
            activeBosses.Add(go.GetComponent<AIController>());
            activeBosses[activeBosses.Count-1].health = activeBosses[0].health;
        }

    }

    void UpdateSharedHealth(){
        float minHealth = activeBosses[0].health;
        Debug.Log("Currently "+ activeBosses.Count+ " active bosses");
        //optimize l8r by having the health only update when hit
        foreach(var controller in activeBosses){
            if(controller.health<minHealth){
                minHealth = controller.health;
            }
        }
        foreach(var controller in activeBosses){
            controller.health = minHealth;
        }
    }
    public void StartSecondPhase(){
        activeBosses[0].transform.position = initialPosition;
        /* if(duplicatePosition!=null) activeBosses[1].transform.position = duplicatePosition.position;
        else activeBosses[1].transform.position = Vector3.zero;*/
    }
    public void StartFSM(){
        finiteStateMachine.running = true;
    }

    public void StopFSM(){
        finiteStateMachine.running = false;
    }

    public void ResetBoss(){
        StopFSM();
        if(activeBosses[0].currentAttack!=null){
            activeBosses[0].currentAttack.InterruptAttack();
        }
        finiteStateMachine.ResetFSM();
        transform.parent.position = initialPosition;
        if(activeBosses.Count>1){
            AIController oldDup = activeBosses[1];
            activeBosses.Remove(activeBosses[1]);
            oldDup.DeleteSelf();
            Destroy(oldDup);
        }
    }
}
