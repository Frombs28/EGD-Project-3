using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalSwingAttackWithHinge : EnemyAttack
{
    public float angle = 45f;
    public float attackSpeed = 100f;
    public float attackTime = 2f;
    public float tickTime = .2f;
    Vector3 originalOffset;
    Vector3 originalRotation;
    public GameObject objectWithMaterial = null;
    public GameObject jointPrefab;
    Rigidbody rb;
    Rigidbody enemyrb;
    HingeJoint joint;
    //Vector3 originalScale;
    private void Start() {
        originalOffset = weapon.transform.localPosition;
        originalRotation = weapon.transform.eulerAngles;
        rb = weapon.GetComponent<Rigidbody>();
        enemyrb = GetComponent<Rigidbody>();
        //originalScale = weapon.transform.localScale;
        StartAttack();
    }
    private void Update()
    {
        //weapon.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //Debug.Log(weapon.GetComponent<AudioSource>().isPlaying);
    }
    public override void StartAttack(){
        weapon.transform.parent = null;
        AudioSource audio = weapon.GetComponent<AudioSource>();
        joint.axis = new Vector3(0,0,1);
        if(audio!=null)audio.Play();
        attackCompleted = false;
        enemyrb.constraints |= RigidbodyConstraints.FreezeRotationY;
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
        rb.AddForce(weapon.transform.right*attackSpeed);
        float currentTime = 0.0f;
        while(currentTime<attackTime){
            currentTime+=tickTime;
            yield return new WaitForSeconds(tickTime);
        }
        /*frameCount = 0;
        parryFrame = 45;
        parryable = false;
        attackCompleted = false;
        if(objectWithMaterial!=null) objectWithMaterial.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        float currentAngle = angle;
        float startTime = Time.realtimeSinceStartup;
        while(currentAngle > 0){
            frameCount++;
            float curTime = Time.realtimeSinceStartup;
            //Debug.Log(curTime-startTime);
            //Debug.Log(frameCount);
            if (frameCount >= parryFrame && !parryable)
            {
                parryable = true;
                //Debug.Log("Parryable!!");
                if(objectWithMaterial!=null) objectWithMaterial.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
            }
            weapon.transform.RotateAround(transform.position, transform.right * -1, attackSpeed*direction*Time.deltaTime);
            currentAngle-=attackSpeed*Time.deltaTime;
            //weapon.transform.localScale = originalScale;
            yield return null;
        }
        parryable = false;
        if(objectWithMaterial!=null){
            objectWithMaterial.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        }*/
        yield return null;
        StartCoroutine("UnSwingSword");
    }

    IEnumerator UnSwingSword()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.AddForce(-weapon.transform.right*attackSpeed);
        float currentTime = 0.0f;
        yield return null;
        while(Mathf.DeltaAngle(weapon.transform.eulerAngles.z,originalRotation.z)>5){
            yield return null;
        }
        attackCompleted = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        weapon.transform.localPosition = originalOffset;
        weapon.transform.eulerAngles = originalRotation;
        enemyrb.constraints &= ~RigidbodyConstraints.FreezeRotationY;
        if(objectWithMaterial!=null) objectWithMaterial.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        weapon.transform.parent = transform;
    }

}
