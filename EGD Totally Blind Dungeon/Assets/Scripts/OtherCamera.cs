using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherCamera : MonoBehaviour
{

    public GameObject player;
    public float height;
    public float distance;
    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 lookAt = playerPos - transform.position;
        Vector3 newPos = playerPos - lookAt.normalized * distance;
        newPos.y = playerPos.y + height;
        transform.position += (newPos - transform.position) * Time.deltaTime * speed;
        //transform.position = newPos;

        lookAt = playerPos - transform.position;
        transform.forward = lookAt.normalized;
    }
}
