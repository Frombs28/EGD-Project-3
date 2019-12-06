using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioZone : MonoBehaviour
{
    private AudioSource aud;
    private AudioClip clip;
    public float fadeTime = 0.5f;

    private void Start()
    {
        aud = GetComponent<AudioSource>();
        clip = aud.clip;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            StopCoroutine("FadeToVol");
            aud.clip = clip;
            StartCoroutine(FadeToVol(fadeTime, aud.volume, 1));
            aud.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    { 
        if (other.tag == "Player")
        {
            StopCoroutine("FadeToVol");
            StartCoroutine(FadeToVol(fadeTime, aud.volume, 0));
        }
    }

    IEnumerator FadeToVol(float aTime, float aStart, float aEnd)
    {
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            aud.volume = Mathf.Lerp(aStart, aEnd, t);
            yield return null;
        }
        aud.volume = aEnd;
        if (aEnd == 0)
        {
            aud.Stop();
        }
    }
}
