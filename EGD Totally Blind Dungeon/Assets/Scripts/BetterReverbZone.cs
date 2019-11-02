using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BetterReverbZone : MonoBehaviour
{
    public AudioMixer mixer;
    private float verbVolume = 0f;
    public float fadeTime = 1f;

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
            StopAllCoroutines();
            mixer.GetFloat("VerbVolume", out verbVolume);
            StartCoroutine(FadeOut(fadeTime, verbVolume));
        }
    }

    IEnumerator FadeIn(float aTime, float aStart)
    {
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            verbVolume = aStart + Mathf.Lerp(0, -aStart, t);
            mixer.SetFloat("VerbVolume", verbVolume);
            yield return null;
        }
        mixer.SetFloat("VerbVolume", 0);
    }

    IEnumerator FadeOut(float aTime, float aStart)
    {
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            verbVolume = Mathf.Lerp(aStart, -80, t);
            mixer.SetFloat("VerbVolume", verbVolume);
            yield return null;
        }
        mixer.SetFloat("VerbVolume", -80);
    }
}
