using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : MonoBehaviour
{
    int charges = 3;
    PlayerHealth player;
    BoxCollider box;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerHealth>();
        box = GetComponent<BoxCollider>();
        box.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Recharge()
    {
        charges = 3;
    }

    public void Heal()
    {
        player.AddHealth();
        charges--;
        Debug.Log("Healed! " + charges + " charges left!");
    }

    public int NumCharges()
    {
        return charges;
    }

    public void Amulet()
    {
        box.enabled = true;
    }
}
