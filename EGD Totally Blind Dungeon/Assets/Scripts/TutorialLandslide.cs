using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLandslide : MonoBehaviour
{
    TutorialManager manager;
    bool done = false;
    public GameObject chest;
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
            manager.Step6();
            chest.SetActive(true);
            done = true;
        }
    }
}