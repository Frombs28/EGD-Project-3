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
            mixer.SetFloat("VerbRoom", room);
            mixer.SetFloat("VerbDecay", decayTime);
            StartCoroutine("FadeIn", fadeTime);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            StopAllCoroutines();
            StartCoroutine("FadeOut", fadeTime);
        }
    }

    IEnumerator FadeIn(float aTime)
    {
        mixer.GetFloat("VerbVolume", out verbVolume);
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            verbVolume = -80 + Mathf.Lerp(0, 80, t);
            mixer.SetFloat("VerbVolume", verbVolume);
            yield return null;
        }
        mixer.SetFloat("VerbVolume", 0);
    }

    IEnumerator FadeOut(float aTime)
    {
        mixer.GetFloat("VerbVolume", out verbVolume);
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            verbVolume = Mathf.Lerp(0, -80, t);
            mixer.SetFloat("VerbVolume", verbVolume);
            yield return null;
        }
        mixer.SetFloat("VerbVolume", -80);
    }
}
