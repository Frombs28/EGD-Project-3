﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemyLocated : MonoBehaviour
{
    TutorialManager manager;
    bool done = false;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<TutorialManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !done)
        {
            done = true;
            manager.Step9();
        }
    }
}
