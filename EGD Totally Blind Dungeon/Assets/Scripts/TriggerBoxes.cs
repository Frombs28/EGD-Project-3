using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TriggerBoxes : MonoBehaviour
{
    //public List<GameObject> Enemies;
    bool go = false;
    string[] bad_arrays;
    Vector3 startPos;
    
    public MoveTo enemy;
    // Start is called before the first frame update
    void Awake()
    {
        /*foreach(GameObject enemy in Enemies)
        {
            //enemy.SetActive(false);
            //enemy.GetComponentInChildren<FiniteStateMachine>().enabled = false;
            //enemy.GetComponent<MoveTo>().enabled = false;
        }*/

        startPos = enemy.initialPos;
        bad_arrays = new string[2];
        bad_arrays[0] = "Player";
        bad_arrays[1] = "Wall";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            RaycastHit hit;
            Vector3 direction = other.transform.position - transform.position;
            int layerMask = LayerMask.GetMask("Wall");
            //layerMask = ~layerMask;
            Debug.DrawRay(transform.position, direction);
            if (Physics.Raycast(transform.position, direction, out hit, Vector3.Distance(transform.position,other.transform.position), layerMask))
            {
                print("Fail - found wall");
            }
            else
            {
                Debug.Log("Player hit trigger: " + gameObject.name);
                enemy.GetComponent<NavMeshAgent>().destination = other.transform.position;
                enemy.pursue = true;
            }
        }
    }
    /*
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && go)
        {
            Debug.Log("Player left trigger: " + gameObject.name);
            enemy.GetComponent<NavMeshAgent>().destination = startPos;
            enemy.pursue = false;
            go = false;
        }
    }
    */
    
}
