using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class EdgeTracker : MonoBehaviour
{
    public AudioSource aud;
    public AudioClip clip;
    public GameObject innerWall;
    public GameObject outerWall;
    public SteamVR_PlayArea playArea;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        
    }
}
