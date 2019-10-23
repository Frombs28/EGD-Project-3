using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRaycast : MonoBehaviour
{
    private AudioSource aud;
    private GameObject player;

    //change this if we need to ignore some layers or something like that
    private int layerMask = LayerMask.GetMask("Wall");
    // Start is called before the first frame update
    void Start()
    {
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
                float dampening = hit.transform.gameObject.GetComponent<AudioMaterial>().dampening;
            }
        }
        
    }
}
