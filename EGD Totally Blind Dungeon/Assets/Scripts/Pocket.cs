using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pocket : MonoBehaviour
{
    bool full;
    public Interact curItem = null;
    public ItemTracker tracker;
    public int id;
    /*
      Each pocket has an id number:
      0 = Back
      1 = Top left
      2 = Top right
      3 = Bottom left
      4 = Bottom right
    */

    // Start is called before the first frame update
    void Start()
    {
        full = false;
        tracker = FindObjectOfType<ItemTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsFull()
    {
        return full;
    }

    public void Fill(Interact item)
    {
        full = true;
        curItem = item;
        item.transform.parent = gameObject.transform;
        item.transform.localPosition = Vector3.zero;
        curItem.GetComponent<Rigidbody>().isKinematic = false;
        curItem.GetComponent<Rigidbody>().useGravity = false;
        tracker.NewPocketItem(curItem,id);
    }

    public void Empty()
    {
        full = false;
        curItem.transform.parent = null;
        curItem.GetComponent<Rigidbody>().isKinematic = true;
        curItem.GetComponent<Rigidbody>().useGravity = true;
        curItem = null;
        tracker.RemovePocketItem(id);
    }
}
