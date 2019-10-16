﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Interact : MonoBehaviour
{
    [HideInInspector]
    public Hand m_ActiveHand = null;
    private AudioMaterial mat;
    private AudioSource aud;

    private void Start()
    {
        aud = gameObject.GetComponent<AudioSource>();
        mat = gameObject.GetComponent<AudioMaterial>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<AudioMaterial>() != null)
        {
            int index = mat.compareMats(collision.gameObject.GetComponent<AudioMaterial>().mat);
            aud.clip = AudioMaster.staticMatSounds[index];
            //aud.pitch = Random.Range(0.8f, 1.2f);
            aud.Play();
        }
    }


}


