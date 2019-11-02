﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public float health;
    public float damage;
    public float velocity;
    public float angular;

    public Rigidbody rb;

    public int behavior;

    public GameObject player;

    public float angle = 10f;
    public float timeCounter = 0;

    public int startFrame;
    public int endFrame;

    public float targetRadiusA = 2f;
    public float slowRadiusA = 2f;
    public float maxRotation = 2f;
    public float timeToTarget = 2f;
    public float maxAngularAcceleration = 2f;
    bool following;
    bool fleeing;
    public bool active = true;

    //public bool parry;

    public EnemyAttack verticalSwing = null;

    public EnemyAttack currentAttack;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody>();
        //velocity = 5;
        if(verticalSwing == null) verticalSwing = gameObject.GetComponent<EnemyAttack>();
        currentAttack = verticalSwing;
        following = false;
        fleeing = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ColorChangeBack()
    {
        GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        MoveTowardPlayer();
    }

    public void SubtractHealth(float sub)
    {
        health -= sub;
        Debug.Log("Hit!");
        GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        if(health <= 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Invoke("ColorChangeBack", 0.5f);
        }
    }

    public bool IsParryable()
    {
        return verticalSwing.parryable;
    }

    public void Stun()
    {
        verticalSwing.InterruptAttack();
    }

    public void MoveCircular()
    {
        //Vector3 yeah = transform.position + new Vector3(Mathf.Sin(transform.rotation.x), 0, Mathf.Cos(gameObject.transform.rotation.z));
        timeCounter += Time.deltaTime;

        float x = Mathf.Sin(Mathf.Deg2Rad * angle * timeCounter) * 3;
        float z = Mathf.Cos(Mathf.Deg2Rad * angle * timeCounter) * 3;

        Vector3 newPos = new Vector3(x, gameObject.transform.position.y, z);
        newPos += player.transform.position;

        transform.position = newPos;
        //Face();
    }

    //public void StopMoving()
    //{
    //    if (rb.velocity.magnitude > 0.1f)
    //    {
    //        if (following)
    //        {

    //        }
    //        else if (fleeing)
    //        {

    //        }
    //    }
    //}

    public void MoveTowardPlayer()
    {
        //transform.LookAt(player.transform);
        Vector3 direction = player.transform.position - gameObject.transform.position;
        direction = new Vector3(direction.x, 0f, direction.z);
        direction.Normalize();
        direction *= velocity * Time.deltaTime;
        //if (!following && fleeing)
        //{
        //    rb
        //}
        rb.AddForce(direction);
        following = true;
        fleeing = false;

    }

    public void MoveAwayFromPlayer()
    {
        //Debug.Log("Away");
        //transform.LookAt(player.transform);
        Vector3 direction = player.transform.position - gameObject.transform.position;
        direction.Normalize();
        direction = new Vector3(direction.x, 0f, direction.z);
        direction *= velocity * Time.deltaTime * -1;
        //if (following && !fleeing)
        //{
        //    direction *= velocity;
        //}
        rb.AddForce(direction);
        following = false;
        fleeing = true;
    }
}
