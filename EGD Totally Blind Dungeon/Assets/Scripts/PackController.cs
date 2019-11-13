using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PackController : AIController
{
    public GameObject [] pack;

    int initialPosition;
    

    // Start is called before the first frame update
    void Start()
    {
        //initialPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        int en = CheckPackNumber();
        if (en == 1)
        {
            gameObject.GetComponent<MoveTo>().SetInital();
            //if (Vector3.Distance()
        }
        //CheckPackNumber();
    }

    public int CheckPackNumber()
    {
        int enabled = 0;
        for (int i = 0; i < pack.Length; i++)
        {
            if(pack[i].activeSelf == true)
            {
                enabled++;
            }
        }
        return enabled;
    }
}
