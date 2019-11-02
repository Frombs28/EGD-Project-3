using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingItemInteract : MonoBehaviour
{
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<Interact>().m_ActiveHand != null)
        {
            rb.useGravity = true;
        }
    }
}
