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
      1 = left
      2 = right
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
        curItem.transform.parent = gameObject.transform;
        curItem.transform.localPosition = Vector3.zero;
        //curItem.GetComponent<Rigidbody>().isKinematic = false;
        curItem.gameObject.layer = LayerMask.NameToLayer("DummyLayer");
        curItem.gameObject.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("DummyLayer");
        curItem.GetComponent<Rigidbody>().useGravity = false;
        tracker.NewPocketItem(curItem,id);
        curItem.pocketMan = gameObject.GetComponent<Pocket>();
    }

    public void Empty()
    {
        full = false;
        curItem.transform.parent = null;
        //curItem.GetComponent<Rigidbody>().isKinematic = true;
        curItem.GetComponent<Rigidbody>().useGravity = true;
        curItem.pocketMan = null;
        curItem.gameObject.layer = LayerMask.NameToLayer("Interactable");
        curItem.gameObject.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("GrabLayer");
        curItem = null;
        tracker.RemovePocketItem(id);
    }
}
