using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioZone : MonoBehaviour
{
    public AudioSource aud;
    public AudioClip clip;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            aud.clip = clip;
            aud.Play();
        }
    }

    private void OnTriggerExit(Collider



































        if (other.tag == "Player")
        {
            aud.Stop();
        }
    }
}
