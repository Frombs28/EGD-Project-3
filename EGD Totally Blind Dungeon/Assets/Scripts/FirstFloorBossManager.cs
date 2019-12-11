using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstFloorBossManager : SimpleObserver
{
    List<AIController> activeBosses;
    Vector3 initialPosition;
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
}
