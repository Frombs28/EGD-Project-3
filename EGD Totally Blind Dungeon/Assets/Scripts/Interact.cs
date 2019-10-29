using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Interact : MonoBehaviour
{
    public AudioSource scrapeyScrape;
    public float offset = 0.25f;

    [HideInInspector]
    public Hand m_ActiveHand = null;
    private AudioMaterial mat;
    private AudioSource aud;
    private Vector3 startPos;

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
            startPos = transform.position;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<AudioMaterial>() != null)
        {
            int scrapeDex = mat.compareMats(collision.gameObject.GetComponent<AudioMaterial>().mat);
            scrapeyScrape.clip = AudioMaster.staticScrapeSounds[scrapeDex];
            if (Vector3.Distance(transform.position, startPos) > offset)
            {
                startPos = transform.position;
                if (!scrapeyScrape.isPlaying)
                {
                    scrapeyScrape.Play();
                }
            }
            else
            {
                scrapeyScrape.Stop();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        scrapeyScrape.Stop();
    }
}


