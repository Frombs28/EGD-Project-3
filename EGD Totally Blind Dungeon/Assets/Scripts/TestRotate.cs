using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotate : MonoBehaviour
{
    Vector3 newPos;
    Vector3 startPos;
    private Rigidbody rgd;
    public float speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        rgd = GetComponent<Rigidbody>();
        startPos = transform.position;
        newPos = new Vector3(2, startPos.y, startPos.z);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log((transform.position - newPos).magnitude);
        if (transform.position.x >= 2)
        {
            newPos = new Vector3(-2, startPos.y, startPos.z);
            Debug.Log("Switch up");
        }
        if (transform.position.x <= -2)
        {
            newPos = new Vector3(2, startPos.y, startPos.z);
            Debug.Log("Switch down");
        }
        rgd.velocity = newPos.normalized * speed * Time.deltaTime;
    }
}
