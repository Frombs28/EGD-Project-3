using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLandslide : MonoBehaviour
{
    TutorialManager manager;
    bool done = false;
    public GameObject chest;
    public AudioSource aud;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<TutorialManager>();
        chest.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !done)
        {
            aud.Play();
            chest.SetActive(true);
            done = true;
            Invoke("Go", aud.clip.length + 1f);
        }
    }

    void Go()
    {
        manager.Step6();
    }
}