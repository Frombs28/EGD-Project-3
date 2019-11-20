using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class EdgeTracker : MonoBehaviour
{
    private AudioSource aud;
    bool playing;
    public GameObject player;
    public GameObject follow;
    public float boundX = 2.5f;
    public float boundY = 2.25f;
    private float currX = 0;
    private float currY = 0;
    public float offset = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       print("Playing: " + playing);
    }

    private void FixedUpdate()
    {
        currX = Mathf.Abs(player.transform.localPosition.x);
        currX *= 10;
        currX = Mathf.Round(currX) / 10;
        currY = Mathf.Abs(player.transform.localPosition.z);
        currY *= 10;
        currY = Mathf.Round(currY) / 10;
        if (!playing)
        {
            if (currX > (boundX / 2))
            {
                aud.Play();
                playing = true;
                return;
            }
            else if (currY > (boundY / 2))
            {
                aud.Play();
                playing = true;
                return;
            }
        }
        else
        {
            if (currX < (boundX / 2) - offset/2 && currY < (boundY / 2) - offset / 2)
            {
                aud.Stop();
                playing = false;
                return;
            }
        }
    }
}
