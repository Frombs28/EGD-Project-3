using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float[] position;
    public int[] items;
    public int crystals;

    public PlayerData(GameObject player)
    {
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
        items = player.GetComponent<ItemTracker>().GetItems();
        crystals = player.GetComponent<ItemTracker>().NumCrystals();
    }
}
