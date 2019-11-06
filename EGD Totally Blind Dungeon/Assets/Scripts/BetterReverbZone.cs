using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BetterReverbZone : MonoBehaviour
{
    public AudioMixer mixer;
    private float verbVolume = 0f;
    public float fadeTime = 1f;
    public float maxVol = 1500f;
    public float minVol = -10000f;

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
            print("here");
            StopAllCoroutines();
            mixer.SetFloat("VerbRoom", room);
            mixer.SetFloat("VerbDecay", decayTime);
            mixer.GetFloat("VerbVolume", out verbVolume);
            StartCoroutine(FadeIn(fadeTime, verbVolume));
        }
    }

    private void OnTriggerExit(Collider other)
    {       
        if (other.tag == "Player")
        {
            print("here2");
            StopAllCoroutines();
            mixer.GetFloat("VerbVolume", out verbVolume);
            StartCoroutine(FadeOut(fadeTime, verbVolume));
        }
    }

    IEnumerator FadeIn(float aTime, float aStart)
    {
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            verbVolume = aStart + Mathf.Lerp(maxVol, -aStart, t);
            mixer.SetFloat("VerbVolume", verbVolume);
            yield return null;
        }
        mixer.SetFloat("VerbVolume", maxVol);
    }

    IEnumerator FadeOut(float aTime, float aStart)
    {
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            verbVolume = Mathf.Lerp(aStart, minVol, t);
            mixer.SetFloat("VerbVolume", verbVolume);
            yield return null;
        }
        mixer.SetFloat("VerbVolume", minVol);
    }
}
