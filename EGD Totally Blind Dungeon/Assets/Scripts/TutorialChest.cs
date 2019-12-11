using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialChest : MonoBehaviour
{
    bool opened = false;
    public MeshRenderer close;
    public MeshRenderer open;
    public Transform spawnPoint;
    public AudioSource aud;
    Healer heal;
    TutorialManager tutMan;

    // Start is called before the first frame update
    void Start()
    {
        open.enabled = false;
        close.enabled = true;
        heal = FindObjectOfType<Healer>();
        tutMan = FindObjectOfType<TutorialManager>();
        //aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Open()
    {
        opened = true;
        close.enabled = false;
        open.enabled = true;
        aud.Stop();
        heal.Amulet();
        tutMan.Step7();
    }

    public bool isOpened()
    {
        return opened;
    }

    public void Restart()
    {
        opened = false;
        open.enabled = false;
        close.enabled = true;
        aud.loop = true;
        aud.Play();
    }
}

