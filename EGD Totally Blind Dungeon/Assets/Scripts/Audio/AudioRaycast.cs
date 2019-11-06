using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRaycast : MonoBehaviour
{
    public string lowpassVarName;
    public float fadeTime = 0.5f;
    private AudioSource aud;
    private GameObject player;
    private Vector3 direction;
    private GameObject currentObj = null;
    private float dampening = 0;
    private float oldDamp = 0;
    private float dampSetVal = 0f;

    //change this if we need to ignore some layers or something like that
    private int layerMask;
    // Start is called before the first frame update
    void Start()
    {
        layerMask = LayerMask.GetMask("Wall");
        aud = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("MainCamera");
        aud.outputAudioMixerGroup.audioMixer.GetFloat(lowpassVarName, out oldDamp);
    }

    // Update is called once per frame
    void Update()
    {
        if (aud.isPlaying)
        {
            RaycastHit hit;
            direction = player.transform.position - transform.position;
            direction = direction.normalized;
            if (Physics.Raycast(transform.position, direction, out hit,
                Vector3.Distance(transform.position, player.transform.position), layerMask))
            {
                if(hit.collider.gameObject != currentObj)
                {
                    StopAllCoroutines();
                    currentObj = hit.collider.gameObject;
                    dampening = hit.transform.gameObject.GetComponent<AudioMaterial>().dampening;
                    dampening = 22000 * Mathf.Pow(dampening, 2);
                    aud.outputAudioMixerGroup.audioMixer.GetFloat(lowpassVarName, out oldDamp);
                    StartCoroutine(FadeTo(fadeTime, oldDamp, dampening));
                }
            }
            else
            {
                if (currentObj != null)
                {
                    currentObj = null;
                    StopAllCoroutines();
                    aud.outputAudioMixerGroup.audioMixer.GetFloat(lowpassVarName, out oldDamp);
                    StartCoroutine(FadeTo(fadeTime, oldDamp, 22000));
                }
            }
        }
        
    }

    IEnumerator FadeTo(float aTime, float aStart, float aEnd)
    {
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            dampSetVal =  Mathf.Lerp(aStart, aEnd, t);
            aud.outputAudioMixerGroup.audioMixer.SetFloat(lowpassVarName, dampSetVal);
            yield return null;
        }
        aud.outputAudioMixerGroup.audioMixer.SetFloat(lowpassVarName, aEnd);
    }
}
