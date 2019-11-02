using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        
        //Time.captureFramerate = 60;
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
