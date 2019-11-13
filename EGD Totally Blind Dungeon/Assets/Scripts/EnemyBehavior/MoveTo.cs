using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour
{
    public Transform player;
    NavMeshAgent agent;
    public float stoppingDistance;
    public Vector3 initialPos;
    public float furthest = 15f;
    public bool pursue = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = transform.position;
        stoppingDistance = GetComponentInChildren<CloseTransition>().minDistance;
        agent.stoppingDistance = stoppingDistance;

        initialPos = transform.position;
        //player = GameObject.FindGameObjectWithTag("MainCamera").transform;
        //furthest = 15f;
    }

    // Update is called once per frame
    void Update()
    {
        //TooFar();
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));    
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 4);
        if (pursue){
            agent.destination = player.position;
            //print(agent.destination);
        }
        else{
            agent.destination = initialPos;
            //print("second line " + agent.destination);
        }
        //agent.destination = player.position;

    }
    public bool IsInRange()
    {
        if (Mathf.Abs((player.transform.position - gameObject.transform.position).magnitude) <= stoppingDistance)
        {
            //Debug.Log((player.transform.position - gameObject.transform.position).magnitude);

            return true;
        }
        return false;
    }
    public void TooFar(){
        if (Vector3.Distance(initialPos, transform.position) > furthest){
            agent.destination = initialPos;
        }
    }
    public void SetInital()
    {
        agent.destination = initialPos;
    }
}
