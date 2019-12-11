using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTorch : MonoBehaviour
{
    TutorialManager manager;
    bool done = false;

    private void Start()
    {
        manager = FindObjectOfType<TutorialManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !done)
        {
            done = true;
            manager.Step12();
        }
    }
}
