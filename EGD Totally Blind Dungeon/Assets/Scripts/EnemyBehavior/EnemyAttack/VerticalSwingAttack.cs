using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalSwingAttack : EnemyAttack
{
    public float angle = 45f;
    public float attackSpeed = 100f;
    public bool startOnBottom = false;
    int direction = 1;
    public bool startInFront = false;
    Vector3 originalOffset;
    Vector3 originalRotation;
    public GameObject objectWithMaterial;
    //Vector3 originalScale;
    private void Start() {
        originalOffset = weapon.transform.localPosition;
        originalRotation = weapon.transform.eulerAngles;
        //originalScale = weapon.transform.localScale;
    }
    private void Update()
    {
        //weapon.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
    public override void StartAttack(){
        attackCompleted = false;
        if(startOnBottom){
            direction = 1;
        }
        else{
            direction = -1;
        }
        //weapon.transform.rotation = transform.rotation;
        weapon.transform.localPosition = originalOffset;
        //if(!startInFront) weapon.transform.RotateAround(transform.position, transform.right, angle*direction*-1);
        StartCoroutine("SwingSword");
    }
    public override void InterruptAttack(){
        StopAllCoroutines();
        parryable = false;
        objectWithMaterial.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        StartCoroutine("UnSwingSword");
    }

    IEnumerator SwingSword(){
        frameCount = 0;
        parryFrame = 45;
        parryable = false;
        attackCompleted = false;
        objectWithMaterial.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        float currentAngle = angle;
        while(currentAngle > 0){
            frameCount++;
            //Debug.Log(frameCount);
            if (frameCount >= parryFrame && !parryable)
            {
                parryable = true;
                //Debug.Log("Parryable!!");
                objectWithMaterial.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
            }
            weapon.transform.RotateAround(transform.position, transform.right * -1, attackSpeed*direction*Time.deltaTime);
            currentAngle-=attackSpeed*Time.deltaTime;
            //weapon.transform.localScale = originalScale;
            yield return null;
        }
        parryable = false;
        objectWithMaterial.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        StartCoroutine("UnSwingSword");
    }

    IEnumerator UnSwingSword()
    {
        float currentAngle = angle;
        while (currentAngle > 0)
        {
            weapon.transform.RotateAround(transform.position, transform.right * -1, -2*attackSpeed * direction * Time.deltaTime);
            currentAngle -= 2*attackSpeed * Time.deltaTime;
            yield return null;
        }
        attackCompleted = true;
        weapon.GetComponent<Rigidbody>().velocity = Vector3.zero;
        weapon.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        weapon.transform.localPosition = originalOffset;
        weapon.transform.eulerAngles = originalRotation;
        objectWithMaterial.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
    }

}
