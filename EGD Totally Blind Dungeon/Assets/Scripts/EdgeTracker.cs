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

    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playing)
        {
            aud.volume = Mathf.Min((player.transform.position - gameObject.transform.position).magnitude, 1f);
        }
        //print("Playing: " + playing);
    }

    private void FixedUpdate()
    {
        currX = Mathf.Abs(player.transform.position.x - follow.transform.position.x);
        currY = Mathf.Abs(player.transform.position.z - follow.transform.position.z);
        if (!playing)
        {
            if (currX > boundX / 2)
            {
                aud.Play();
                playing = true;
            }
            else if (currY > boundY / 2)
            {
                aud.Play();
                playing = true;
            }
        }
        else
        {
            if (currX <= boundX / 2)
            {
                aud.Stop();
                playing = false;
            }
            else if (currY <= boundY / 2)
            {
                aud.Stop();
                playing = false;
            }
        }
    }
}
