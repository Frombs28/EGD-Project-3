using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialShrine : MonoBehaviour
{
    TutorialManager manager;
    bool done = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && !done)
        {
            done = true;
            manager.Step11();
        }
    }
}
