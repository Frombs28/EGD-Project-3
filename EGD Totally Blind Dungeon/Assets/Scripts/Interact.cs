using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

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
    bool first_time = true;
    public float amplitude1 = 0.08f;
    public float frequency1 = 500f;
    public float amplitude2 = 0.02f;
    public float frequency2 = 20f;
    public int TAP_HAPTIC;
    int firstTime;

    private void Start()
    {
        aud = gameObject.GetComponent<AudioSource>();
        mat = gameObject.GetComponent<AudioMaterial>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<AudioMaterial>() != null && m_ActiveHand != null)
        {
            int index = mat.compareMats(collision.gameObject.GetComponent<AudioMaterial>().mat);
            aud.clip = AudioMaster.staticMatSounds[index];
            //aud.pitch = Random.Range(0.8f, 1.2f);
            aud.Play();
            //Debug.Log("Playing Touch");
            startPos = transform.position;
            m_ActiveHand.m_VibrateAction.Execute(0f,0.05f,frequency1,amplitude1,m_ActiveHand.source);
            firstTime = TAP_HAPTIC;
            //m_ActiveHand.vibrate.
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<AudioMaterial>() != null && m_ActiveHand != null)
        {
            if (firstTime>0)
            {
                firstTime--;
            }
            else
            {
                m_ActiveHand.m_VibrateAction.Execute(0f, 0.1f, frequency2, amplitude2, m_ActiveHand.source);
            }
            int scrapeDex = mat.compareMats(collision.gameObject.GetComponent<AudioMaterial>().mat);
            scrapeyScrape.clip = AudioMaster.staticScrapeSounds[scrapeDex];
            if (Vector3.Distance(transform.position, startPos) > offset /*&& gameObject.GetComponent<Rigidbody>().velocity.magnitude > 1.0f*/)
            {
                startPos = transform.position;
                if (!scrapeyScrape.isPlaying)
                {
                    scrapeyScrape.Play();
                    //Debug.Log("Playing Scrape");
                }
            }
            else
            {
                scrapeyScrape.Stop();
                //Debug.Log("Stopping Scrape1");
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        scrapeyScrape.Stop();
        //Debug.Log("Stopping Scrape2");
    }

    private void Update()
    {
        if(m_ActiveHand == null && first_time)
        {
            aud.clip = AudioMaster.staticMatSounds[0];
            aud.loop = true;
            aud.Play();
            first_time = false;
        }
        else if(m_ActiveHand != null && !first_time)
        {
            aud.Stop();
            aud.loop = false;
            first_time = true;
        }
    }
}


