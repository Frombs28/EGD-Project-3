using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRaycast : MonoBehaviour
{
    public string lowpassVarName;
    private AudioSource aud;
    private GameObject player;

    //change this if we need to ignore some layers or something like that
    private int layerMask;
    // Start is called before the first frame update
    void Start()
    {
        layerMask = LayerMask.GetMask("Wall");
        aud = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (aud.isPlaying)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, player.transform.position, out hit,
                Vector3.Distance(transform.position, player.transform.position), layerMask))
            {
                print(hit.collider.gameObject.name);
                //print(hit.collider.gameObject.layer);
                float dampening = hit.transform.gameObject.GetComponent<AudioMaterial>().dampening;
                //print(dampening);

                //get audio mixer and dampen by using low passes and such
                aud.outputAudioMixerGroup.audioMixer.SetFloat(lowpassVarName, 22000 * Mathf.Pow(dampening, 2));
                /*float temp;
                print(aud.outputAudioMixerGroup.audioMixer.GetFloat("BGMLowpass", out temp));
                print(temp);*/
            }
            else
            {
                aud.outputAudioMixerGroup.audioMixer.SetFloat(lowpassVarName, 22000);
            }
        }
        
    }
}
