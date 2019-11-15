using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject item;
    bool opened = false;
    public MeshRenderer close;
    public MeshRenderer open;
    public Transform spawnPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        open.enabled = false;
        close.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        opened = true;
        close.enabled = false;
        open.enabled = true;
        GameObject newItem = Instantiate(item, spawnPoint.position, Quaternion.identity);
        newItem.GetComponent<Interact>().newStartPos(Vector3.zero);
    }

    public bool isOpened()
    {
        return opened;
    }

    public void Restart()
    {
        opened = false;
        open.enabled = false;
        close.enabled = true;
    }
}
