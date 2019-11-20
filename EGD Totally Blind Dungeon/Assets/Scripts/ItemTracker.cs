using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemTracker : MonoBehaviour
{
    int[] items;
    int checkpoint;
    public GameObject[] itemIndices;
    public List<GameObject> enemies;
    public Hand rightHand;
    public Hand leftHand;
    public Pocket pocket0;
    public Pocket pocket1;
    public Pocket pocket2;
    public Pocket pocket3;
    public Pocket pocket4;
    public GameObject player;
    public StickManipulation stick;
    /////////////////////////////////////////////////////////////////////////////////
    /*
    **  0: Nothing
    **  1: Sword
    **
    */
    /////////////////////////////////////////////////////////////////////////////////
    public GameObject sword;
    public InteractManager im;

    private void Start()
    {
        items = new int[7];
        items[0] = 0;
        items[1] = 0;
        items[2] = 0;
        items[3] = 0;
        items[4] = 0;
        items[5] = 0;
        items[6] = 0;
        checkpoint = PlayerPrefs.GetInt("Checkpoint", 0);
        if(checkpoint > 0)
        {
            print("Loading...");
            LoadPlayer();
        }
        else
        {
            SaveSystem.SavePlayer(this.gameObject);
            PlayerPrefs.SetInt("Checkpoint", 0);
            print("Saving new game!");
        }
        enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        im = FindObjectOfType<InteractManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.SetInt("Checkpoint", 0);
            print("Resetting Game!");
            /*
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
            */
        }

        // FOR TESTING
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SavePlayer();
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

    public void NewPocketItem(Interact item, int pocketNum)
    {
        items[pocketNum + 2] = item.itemIndex;
    }

    public void RemoveLeftHandItem()
    {
        items[0] = 0;
    }

    public void RemoveRightHandItem()
    {
        items[1] = 0;
    }

    public void RemovePocketItem(int pocketNum)
    {
        items[pocketNum + 2] = 0;
    }

    public int[] GetItems()
    {
        return items;
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this.gameObject);
        PlayerPrefs.SetInt("Checkpoint", checkpoint+1);
        print("Saving here!");
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        stick.canMove = false;
        player.transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
        im.Restart();
        items = data.items;
        print("Loaded location: " + player.transform.position);
        if(items[0] > 0)
        {
            // spawn left hand item
            GameObject newItem = Instantiate(itemIndices[items[0] - 1], leftHand.gameObject.transform.position, Quaternion.identity);
            leftHand.spawningItem(newItem.GetComponent<Interact>());
            print("Item in left hand: " + newItem.name);
        }
        if(items[1] > 0)
        {
            // spawn right hand item
            GameObject newItem = Instantiate(itemIndices[items[1] - 1], rightHand.gameObject.transform.position, Quaternion.identity);
            rightHand.spawningItem(newItem.GetComponent<Interact>());
            print("Item in right hand: " + newItem.name);
        }
        if(items[2] > 0)
        {
            // spawn back pocket item
            GameObject newItem = Instantiate(itemIndices[items[2] - 1], pocket0.gameObject.transform.position, Quaternion.identity);
            pocket0.Fill(newItem.GetComponent<Interact>());
            print("Item in back pocket: " + newItem.name);
        }
        if (items[3] > 0)
        {
            // spawn top left pocket item
            GameObject newItem = Instantiate(itemIndices[items[3] - 1], pocket1.gameObject.transform.position, Quaternion.identity);
            pocket1.Fill(newItem.GetComponent<Interact>());
            print("Item in top left pocket: " + newItem.name);
        }
        if (items[4] > 0)
        {
            // spawn top right pocket item
            GameObject newItem = Instantiate(itemIndices[items[4] - 1], pocket2.gameObject.transform.position, Quaternion.identity);
            pocket2.Fill(newItem.GetComponent<Interact>());
            print("Item in top right pocket: " + newItem.name);
        }
        if (items[5] > 0)
        {
            // spawn bottom right pocket item
            GameObject newItem = Instantiate(itemIndices[items[5] - 1], pocket3.gameObject.transform.position, Quaternion.identity);
            pocket3.Fill(newItem.GetComponent<Interact>());
            print("Item in bottom left pocket: " + newItem.name);
        }
        if (items[6] > 0)
        {
            // spawn back pocket item
            GameObject newItem = Instantiate(itemIndices[items[6] - 1], pocket4.gameObject.transform.position, Quaternion.identity);
            pocket4.Fill(newItem.GetComponent<Interact>());
            print("Item in bottom right pocket: " + newItem.name);
        }

        foreach (GameObject enemy in enemies){
            enemy.SetActive(true);
            AIController ai = enemy.GetComponent<AIController>();
            if(ai != null)
            {
                enemy.transform.position = ai.originPos;
            }
        }
        stick.canMove = true;
    }

}
