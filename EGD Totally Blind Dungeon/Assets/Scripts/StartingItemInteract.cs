using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingItemInteract : MonoBehaviour
{
    Rigidbody rb;
    Interact item;
    TutorialManager manager;
    bool found = false;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("StopGravity", 0.5f);
        item = GetComponent<Interact>();
        manager = FindObjectOfType<TutorialManager>();
        gameObject.transform.Rotate(0, 0, -90f);
    }

    // Update is called once per frame
    void Update()
    {
        if(item.m_ActiveHand != null && !found)
        {
            found = true;
            manager.Step4();
        }
    }

    void StopGravity()
    {
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
    }
}
