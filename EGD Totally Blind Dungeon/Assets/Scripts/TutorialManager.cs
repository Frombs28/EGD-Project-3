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
        if(PlayerPrefs.GetInt("Checkpoint", 0) > 0)
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
        if(controller.moving && !walked)
        {
            walked = true;
            Invoke("Step3", 0.5f);
        }
    }

    public void Step1()
    {
        // Intro Dialogue
        Invoke("Step2", 10f);   // Instead of 10f, replace this with the length of the first dialogue clip
    }

    public void Step2()
    {
        Instantiate(sword, new Vector3(spawnSword.position.x, spawnSword.position.y, spawnSword.position.z), Quaternion.identity);
        // How to walk Dialogue
        Invoke("EnableWalking", 10f); // Instead of 10f, replace this with the length of this dialogue clip
    }

    public void Step3()
    {
        // Grab Dialogue
    }

    public void Step4()
    {
        // Let's go fight the baddy Dialogue: go towards the water
        // Transfer Hands Dialogue
        // Sheath Sword Dialogue
    }

    public void Step5()
    {
        // How to tap Dialogue
    }

    public void Step6()
    {
        // UH OH LANDSLIDE Dialogue
        health.SubtractHealth(1);
        health.SubtractHealth(1);
        // How to open chest Dialogue
    }

    public void Step7()
    {
        // How to heal Dialogue
    }

    public void Step8()
    {
        // Let's try right instead Dialouge
    }

    public void Step9()
    {
        controller.canMove = false;
        // It's an enemy Dialogue
        Invoke("EnableWalking", 10f); // Instead of 10f, replace this with the length of this dialogue clip
        Invoke("Step10", 10f); // Instead of 10f, replace this with the length of this dialogue clip
    }

    public void Step10()
    {
        // You killed him Dialogue
        // Go to the Shrine Dialogue
    }

    public void Step11()
    {
        // This is a shrine Dialogue
        // There must be a door around here somewhere Dialogue
    }

    public void Step12()
    {
        controller.canMove = false;
        // This is a torch Dialogue
        // I can't go any further Dialogue
        Invoke("EnableWalking", 10f); // Instead of 10f, replace this with the length of this dialogue clip
    }

    void EnableWalking()
    {
        controller.canMove = true;
    }
}
