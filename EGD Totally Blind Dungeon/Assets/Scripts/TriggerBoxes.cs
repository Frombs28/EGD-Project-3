using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class TriggerBoxes : MonoBehaviour
{
    //public List<GameObject> Enemies;
    bool go = false;
    string[] bad_arrays;
    Vector3 startPos;
    private AudioSource aud;
    public AudioClip[] screams;
    private bool wall = false;
    private GameObject player;
    
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

    private void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (wall && !enemy.pursue)
        {
            RaycastHit hit;
            Vector3 direction = player.transform.position - transform.position;
            int layerMask = LayerMask.GetMask("Wall");
            //layerMask = ~layerMask;
            Debug.DrawRay(transform.position, direction);
            if (Physics.Raycast(transform.position, direction, out hit, Vector3.Distance(transform.position, player.transform.position), layerMask))
            {
                print("Fail - found wall");
            }
            else
            {
                int index = Random.Range(0, screams.Length);
                aud.clip = screams[index];
                aud.loop = false;
                aud.Play();
                Debug.Log("Player hit trigger: " + gameObject.name);
                enemy.GetComponent<NavMeshAgent>().destination = player.transform.position;
                enemy.pursue = true;
                wall = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !enemy.pursue)
        {
            player = other.gameObject;
            RaycastHit hit;
            Vector3 direction = other.transform.position - transform.position;
            int layerMask = LayerMask.GetMask("Wall");
            //layerMask = ~layerMask;
            Debug.DrawRay(transform.position, direction);
            if (Physics.Raycast(transform.position, direction, out hit, Vector3.Distance(transform.position,other.transform.position), layerMask))
            {
                print("Fail - found wall");
                wall = true;
            }
            else
            {
                int index = Random.Range(0, screams.Length);
                aud.clip = screams[index];
                aud.loop = false;
                aud.Play();
                Debug.Log("Player hit trigger: " + gameObject.name);
                enemy.GetComponent<NavMeshAgent>().destination = other.transform.position;
                enemy.pursue = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            wall = false;
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
