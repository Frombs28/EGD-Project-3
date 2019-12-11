using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoor : MonoBehaviour
{
    ItemTracker it;
    bool done = false;
    // Start is called before the first frame update
    void Start()
    {
        it = FindObjectOfType<ItemTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        if(it.NumCrystals() == 2 && !done)
        {
            done = true;
            this.gameObject.SetActive(false);
        }
    }
}
