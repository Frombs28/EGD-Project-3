using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnState : State
{
    public GameObject spawnPrefab;
    public Vector3 spawnLocation = Vector3.zero;
    public override void DoAction(AIController ai){}
    public override void DoEntryAction(AIController ai)
    {
        Debug.Log("Gonna go spawn a boy xd");
        GameObject spawned = Instantiate(spawnPrefab, spawnLocation, Quaternion.identity);
        NotifyObservers(NotificationType.SpawnedItem, "", spawned);
    }
    public override void DoExitAction(AIController ai){}
}
