using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundCorner : MonoBehaviour
{
    public Rigidbody rb;
    public float rightDistance;
    public float forwardDistance;
    public float endDistRight;
    public float endDistForward;

    public bool hitCorner = false;
    bool done = false;
    public bool start = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        endDistForward = transform.position.x + forwardDistance;

    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            if (!hitCorner)
            {
                transform.position = new Vector3(transform.position.x + forwardDistance * Time.deltaTime, transform.position.y, transform.position.z);
                if (transform.position.x <= endDistForward)
                {
                    endDistRight = transform.position.z + rightDistance;
                    hitCorner = true;
                }

            }
            else if (hitCorner && !done)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + rightDistance * Time.deltaTime);
                if (transform.position.z >= endDistRight)
                {
                    done = true;
                }
            }
        }
    }
}
