using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioZone : MonoBehaviour
{
    private AudioSource aud;
    private AudioClip clip;

    private void Start()
    {
        aud = GetComponent<AudioSource>();
        clip = aud.clip;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            aud.clip = clip;
            aud.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    { 
        if (other.tag == "Player")
        {
            aud.Stop();
        }
    }
}
