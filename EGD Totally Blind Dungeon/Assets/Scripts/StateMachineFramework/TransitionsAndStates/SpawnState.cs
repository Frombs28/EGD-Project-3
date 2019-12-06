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
        Instantiate(spawnPrefab, spawnLocation, Quaternion.identity);
    }
    public override void DoExitAction(AIController ai){}
}
