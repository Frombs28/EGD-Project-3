using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BetterReverbZone : MonoBehaviour
{
    public AudioMixer mixer;
    private float verbVolume = 0f;

    //vars to change
    [Range(-10000,0)]
    public float room = -10000f;
    [Range(0.1f, 20f)]
    public float decayTime = 1f;
    //add move vars here

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            mixer.SetFloat("VerbRoom", room);
            mixer.SetFloat("VerbDecay", decayTime);
            StartCoroutine("FadeIn");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            StopAllCoroutines();
            StartCoroutine("FadeOut");
        }
    }

    IEnumerator FadeIn()
    {
        mixer.GetFloat("VerbVolume", out verbVolume);
        while (verbVolume < 0)
        {
            mixer.SetFloat("VerbVolume", verbVolume + 5f);
            yield return new WaitForSeconds(.05f);
        }
        mixer.SetFloat("VerbVolume", 0);
    }

    IEnumerator FadeOut()
    {
        mixer.GetFloat("VerbVolume", out verbVolume);
        while (verbVolume > -80)
        {
            mixer.SetFloat("VerbVolume", verbVolume - 5f);
            yield return new WaitForSeconds(.05f);
        }
        mixer.SetFloat("VerbVolume", -80);
    }
}
