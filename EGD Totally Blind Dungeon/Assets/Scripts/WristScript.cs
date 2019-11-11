using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WristScript : MonoBehaviour
{
    bool swinging;
    Vector3 curPos;
    Vector3 secondPos;
    Vector3 thirdPos;
    public float swingingVal;

    Vector3 myVector;
    Vector3 lastVector;
    public float angleVal;

    float magnitude;
    float angle;

    int frameForgive;
    public int FRAMES = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        curPos = transform.position;
        secondPos = transform.position;
        thirdPos = transform.position;
        myVector = Vector3.zero;
        lastVector = Vector3.zero;
        swinging = false;
        frameForgive = 0;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!swinging)
        {
            thirdPos = secondPos;
            secondPos = curPos;
            curPos = transform.position;
            lastVector = myVector;
            myVector = curPos - secondPos;

            magnitude = myVector.magnitude;
            angle = Vector3.Angle(myVector, lastVector);

            if (magnitude >= swingingVal && angle <= angleVal)
            {
                //print("MAGNITUDE: " + magnitude);
                //print("ANGLE: " + angle);
                swinging = true;
                frameForgive = 0;
            }
        }
        else if(frameForgive >= FRAMES)
        {
            thirdPos = secondPos;
            secondPos = curPos;
            curPos = transform.position;
            lastVector = myVector;
            myVector = curPos - secondPos;

            magnitude = myVector.magnitude;
            angle = Vector3.Angle(myVector, lastVector);
            if (magnitude >= swingingVal && angle <= angleVal)
            {
                swinging = true;
                frameForgive = 0;
            }
            else
            {
                swinging = false;
            }

        }
        else
        {
            frameForgive++;
        }
        print(swinging);
    }

    public bool IsSwinging()
    {
        return swinging;
    }
}
