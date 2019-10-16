using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMaster : MonoBehaviour
{
    public AudioClip[] matSounds;
    public static AudioClip[] staticMatSounds;
    // Start is called before the first frame update
    void Start()
    {
        staticMatSounds = matSounds;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
