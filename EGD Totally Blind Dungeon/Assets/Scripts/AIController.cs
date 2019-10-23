﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public float health;
    public float velocity;
    public float angular;

    public Rigidbody rb;

    public int behavior;

    public GameObject player;

    public float targetRadiusA = .5f;
    public float slowRadiusA = 1f;
    public float maxRotation = 5f;
    public float timeToTarget = 2f;
    public float maxAngularAcceleration = 3f;

    public float angle = 10f;
    public float timeCounter = 0;

    public int startFrame;
    public int endFrame;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody>();
        velocity = 5;
    }

    // Update is called once per frame
    void Update()
    {
        //MoveAwayFromPlayer();
        //Face();
        MoveCircular();
    }

    void SubtractHealth(float sub)
    {
        health -= sub;
    }

    public void MoveCircular()
    {
        //Vector3 yeah = transform.position + new Vector3(Mathf.Sin(transform.rotation.x), 0, Mathf.Cos(gameObject.transform.rotation.z));
        timeCounter += Time.deltaTime;

        float x = Mathf.Sin(Mathf.Deg2Rad * angle * timeCounter) * 3;
        float z = Mathf.Cos(Mathf.Deg2Rad * angle * timeCounter) * 3;
        //float x = Mathf.Cos(timeCounter) * 3;
        //float z = Mathf.Sin(timeCounter) * 3;

        Vector3 newPos = new Vector3(x, 0f, z);
        newPos += player.transform.position;

        transform.position = newPos;

        //Vector3 center = player.transform.position;
        //var offset = new Vector3(Mathf.Sin(angle), 0f, Mathf.Cos(angle)) * 4;
        //transform.position = center + offset;
        //Vector3 direction = transform.right - transform.position;
        //direction = new Vector3(direction.x, 0f, direction.z);
        //direction.Normalize();
        //direction *= velocity * Time.deltaTime;
        //rb.AddForce(direction);
        //transform.position += (transform.right / 10);
        Face();
    }

    void MoveTowardPlayer()
    {
        //transform.LookAt(player.transform);
        Vector3 direction = player.transform.position - gameObject.transform.position;
        direction = new Vector3(direction.x, 0f, direction.z);
        direction.Normalize();
        direction *= velocity * Time.deltaTime;
        rb.AddForce(direction);

    }

    void MoveAwayFromPlayer()
    {
        //Debug.Log("Away");
        //transform.LookAt(player.transform);
        Vector3 direction = player.transform.position - gameObject.transform.position;
        direction.Normalize();
        Debug.Log(direction);
        direction = new Vector3(direction.x, 0f, direction.z);
        direction *= velocity * Time.deltaTime * -1;
        rb.AddForce(direction);
        Face();
    }

    void Face()
    {
        Debug.Log("Face");
        float orientation;
        float targetRotation;

        //Sets the direction you need to face based upon the target
        Vector3 direction = player.transform.position - gameObject.transform.position;
        if (direction.magnitude == 0)
        {
            orientation = 0;
        }

        //Subtracts the agent's current orientation from the place it needs to go
        float orient = Mathf.Atan2(direction.x, direction.z);
        orient -= gameObject.transform.rotation.y;
        orient = turnToAngle(orient);

        //Finds if the acceleration needs to slow down or if the agent is in the right direction
        float absoluteOrient = Mathf.Abs(orient);
        if (absoluteOrient < (targetRadiusA))
        {
            orientation = 0;
        }
        if (absoluteOrient > (slowRadiusA))
        {
            targetRotation = maxRotation;
        }
        else
        {
            targetRotation = maxRotation * absoluteOrient / slowRadiusA;
        }


        targetRotation *= orient / absoluteOrient;
        float angular = targetRotation - gameObject.transform.rotation.y;
        angular /= timeToTarget;

        //Checks if the acceleration is too great, fixes it to match if it is not
        float angularAcceleration = Mathf.Abs(angular);
        if (angularAcceleration > maxAngularAcceleration)
        {
            angular /= angularAcceleration;
            angular *= maxAngularAcceleration;
        }
        orientation = angular;
        rb.MoveRotation(Quaternion.Euler(new Vector3(0, Mathf.Rad2Deg * orientation, 0)));
    }
    public float turnToAngle(float f)
    {
        //Is used in multiple functions, helps turn to the angle to something the agent can spin to
        //without spinning in circles
        float turn = Mathf.PI * 2;
        while (f > Mathf.PI)
        {
            f -= turn;
        }
        while (f < -Mathf.PI)
        {
            f += turn;
        }
        return f;
    }

}
