﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemy : MonoBehaviour
{
    TutorialManager manager;
    bool done = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Death()
    {
        if (!done)
        {
            done = true;
            manager.Step10();
        }
    }

}
