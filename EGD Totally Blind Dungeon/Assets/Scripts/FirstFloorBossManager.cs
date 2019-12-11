using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstFloorBossManager : SimpleObserver
{
    List<AIController> activeBosses;
    Vector3 initialPosition;
    public Transform duplicatePosition = null;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        initialPosition = transform.position;
        AIController main = transform.parent.GetComponent<AIController>();
        activeBosses = new List<AIController>();
        activeBosses.Add(main);
        if(duplicatePosition!=null) GetComponentInChildren<SpawnState>().spawnLocation = duplicatePosition.position;
        else GetComponentInChildren<SpawnState>().spawnLocation = Vector3.zero;
    }
    private void Update() {
        UpdateSharedHealth();
    }
    public override void OnNotify(NotificationType notice, string message, GameObject go){
        if(notice == NotificationType.SpawnedItem){
            Debug.Log("The chasey fella should be spawned");
            activeBosses.Add(go.GetComponent<AIController>());
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
}
