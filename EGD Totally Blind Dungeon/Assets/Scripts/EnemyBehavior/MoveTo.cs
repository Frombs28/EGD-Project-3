using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour
{
    public Transform player;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = player.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsInRange())
        {
            //Debug.Log("Range");
            //How we want it to move goes here

            //gameObject.GetComponent<AIController>().MoveCircular();
        }
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));    // flattens the vector3
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 4);

    }
    public bool IsInRange()
    {
        if (Mathf.Abs((player.transform.position - gameObject.transform.position).magnitude) < 4f)
        {
            Debug.Log((player.transform.position - gameObject.transform.position).magnitude);

            return true;
        }
        return false;
    }
}
