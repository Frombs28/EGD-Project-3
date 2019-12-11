using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{

    AudioSource aud;
    public GameObject sword;
    public Transform spawnSword;
    public StickManipulation controller;
    bool walked = false;
    public PlayerHealth health;
    public List<AudioClip> clips;
    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
        if (PlayerPrefs.GetInt("Checkpoint", 0) == 0)
        {
            Begin();
        }
    }

    public void Begin()
    {
        controller.canMove = false;
        Invoke("Step1", 4f);
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.moving && !walked)
        {
            walked = true;
            Invoke("Step3", 0.5f);
        }
    }

    public void Step1()
    {
        if (aud.isPlaying)
        {
            aud.Stop();
        }
        aud.clip = clips[0];
        aud.Play();
        Invoke("Step2", clips[0].length + 1f);
    }

    public void Step2()
    {
        Instantiate(sword, new Vector3(spawnSword.position.x, spawnSword.position.y, spawnSword.position.z), Quaternion.identity);
        if (aud.isPlaying)
        {
            aud.Stop();
        }
        aud.clip = clips[1];
        aud.Play();
        Invoke("EnableWalking", clips[1].length + 1f);
    }

    public void Step3()
    {
        if (aud.isPlaying)
        {
            aud.Stop();
        }
        aud.clip = clips[2];
        aud.Play();
    }

    public void Step4()
    {
        if (aud.isPlaying)
        {
            aud.Stop();
        }
        aud.clip = clips[3];
        aud.Play();
    }

    public void Step5()
    {
        if (aud.isPlaying)
        {
            aud.Stop();
        }
        aud.clip = clips[4];
        aud.Play();
    }

    public void Step6()
    {
        health.SubtractHealth(1);
        health.SubtractHealth(1);
        if (aud.isPlaying)
        {
            aud.Stop();
        }
        aud.clip = clips[5];
        aud.Play();
    }

    public void Step7()
    {
        if (aud.isPlaying)
        {
            aud.Stop();
        }
        aud.clip = clips[6];
        aud.Play();
    }

    public void Step8()
    {
        if (aud.isPlaying)
        {
            aud.Stop();
        }
        aud.clip = clips[7];
        aud.Play();
    }

    public void Step9()
    {
        controller.canMove = false;
        if (aud.isPlaying)
        {
            aud.Stop();
        }
        aud.clip = clips[8];
        aud.Play();
        Invoke("EnableWalking", clips[8].length + 1f);
        Invoke("Step10", clips[8].length + 1f);
    }

    public void Step10()
    {
        if (aud.isPlaying)
        {
            aud.Stop();
        }
        aud.clip = clips[9];
        aud.Play();
    }

    public void Step11()
    {
        if (aud.isPlaying)
        {
            aud.Stop();
        }
        aud.clip = clips[10];
        aud.Play();
    }

    public void Step12()
    {
        controller.canMove = false;
        if (aud.isPlaying)
        {
            aud.Stop();
        }
        aud.clip = clips[11];
        aud.Play();
        Invoke("EnableWalking", clips[11].length + 1f); // Instead of 10f, replace this with the length of this dialogue clip
    }

    void EnableWalking()
    {
        controller.canMove = true;
    }
}
