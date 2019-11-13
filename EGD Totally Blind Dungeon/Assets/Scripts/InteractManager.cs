using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractManager : MonoBehaviour
{
    public List<Interact> droppedInteractables;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Add(Interact dropped)
    {
        droppedInteractables.Add(dropped);
    }

    public void Restart()
    {
        foreach(Interact item in droppedInteractables)
        {
            item.Restart();
        }
    }
}
