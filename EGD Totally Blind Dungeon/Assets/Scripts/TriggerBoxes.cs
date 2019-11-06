using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBoxes : MonoBehaviour
{
    public List<GameObject> Enemies;
    bool go = false;
    
    // Start is called before the first frame update
    void Awake()
    {
        foreach(GameObject enemy in Enemies)
        {
            //enemy.SetActive(false);
            enemy.GetComponentInChildren<FiniteStateMachine>().enabled = false;
            enemy.GetComponent<MoveTo>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !go)
        {
            Debug.Log("Player hit trigger: " + gameObject.name);
            foreach (GameObject enemy in Enemies)
            {
                enemy.GetComponentInChildren<FiniteStateMachine>().enabled = true;
                enemy.GetComponent<MoveTo>().enabled = true;
            }
            go = true;
        }
    }
}
