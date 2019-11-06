using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTracker : MonoBehaviour
{
    int[] items;
    int checkpoint;
    public GameObject[] itemIndices;
    public Hand rightHand;
    public Hand leftHand;
    /////////////////////////////////////////////////////////////////////////////////
    /*
    **  0: Nothing
    **  1: Sword
    **
    */
    /////////////////////////////////////////////////////////////////////////////////
    public GameObject sword;

    private void Start()
    {
        items = new int[2];
        items[0] = 0;
        items[1] = 0;
        checkpoint = PlayerPrefs.GetInt("Checkpoint", 0);
        if(checkpoint > 0)
        {
            LoadPlayer();
        }
    }

    public void NewLeftHandItem(Interact item)
    {
        items[0] = item.itemIndex;
    }

    public void NewRightHandItem(Interact item)
    {
        items[1] = item.itemIndex;
    }

    public int[] GetItems()
    {
        return items;
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this.gameObject);
        PlayerPrefs.SetInt("Checkpoint", checkpoint+1);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        gameObject.transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
        items = data.items;
        if(items[0] > 0)
        {
            // spawn left hand item
            GameObject newItem = Instantiate(itemIndices[items[0] - 1], leftHand.gameObject.transform.position, Quaternion.identity);
            leftHand.spawningItem(newItem.GetComponent<Interact>());
        }
        if(items[1] > 0)
        {
            // spawn right hand item
            GameObject newItem = Instantiate(itemIndices[items[1] - 1], rightHand.gameObject.transform.position, Quaternion.identity);
            rightHand.spawningItem(newItem.GetComponent<Interact>());
        }

    }

}
