using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMaster : MonoBehaviour
{
    public AudioClip[] matSounds;
    public static AudioClip[] staticMatSounds;
    public AudioClip[] scrapeSounds;
    public static AudioClip[] staticScrapeSounds;
    public AudioClip[] woodFootsteps;
    public AudioClip[] metalFootsteps;
    public AudioClip[] stoneFootsteps;
    public AudioClip[] grassFootsteps;
    public static List<AudioClip[]> staticFootstepSounds;
    // Start is called before the first frame update
    void Start()
    {
        staticMatSounds = matSounds;
        staticScrapeSounds = scrapeSounds;
        staticFootstepSounds = new List<AudioClip[]>();
        staticFootstepSounds.Add(woodFootsteps);
        staticFootstepSounds.Add(metalFootsteps);
        staticFootstepSounds.Add(stoneFootsteps);
        staticFootstepSounds.Add(grassFootsteps);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
