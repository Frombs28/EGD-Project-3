using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstFloorBossManager : SimpleObserver
{
    public override void OnNotify(NotificationType notice, string message, float value){
        if(notice == NotificationType.SpawnedItem){
            //do do something
        }
    }
}
