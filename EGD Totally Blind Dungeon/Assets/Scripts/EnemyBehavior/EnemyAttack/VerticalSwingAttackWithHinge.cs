using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalSwingAttackWithHinge : EnemyAttack
{
    public float angle = 45f;
    public float attackSpeed = 100f;
    public float attackTime = 2f;
    public float tickTime = .2f;
    public float downMovementSpeed = .5f;
    Vector3 originalOffset;
    Vector3 originalRotation;
    public GameObject objectWithMaterial = null;
    public GameObject jointPrefab;
    public GameObject swordPosition;
    Rigidbody rb;
    Rigidbody enemyrb;
    ConfigurableJoint joint;
    bool movingWrist = false;
    //Vector3 originalScale;
    private void Start() {
        originalOffset = weapon.transform.localPosition;
        originalRotation = weapon.transform.eulerAngles;
        rb = weapon.GetComponent<Rigidbody>();
        enemyrb = GetComponent<Rigidbody>();
        //originalScale = weapon.transform.localScale;
        //StartAttack();
    }
    private void Update()
    {
        if(!movingWrist) weapon.transform.localPosition = originalOffset;
        //weapon.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //Debug.Log(weapon.GetComponent<AudioSource>().isPlaying);
    }
    public override void StartAttack(){
        weapon.transform.parent = null;
        AudioSource audio = weapon.GetComponent<AudioSource>();
        if(audio!=null)audio.Play();
        attackCompleted = false;
        //enemyrb.constraints |= RigidbodyConstraints.FreezeRotationY;
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
        GameObject jointGO = Instantiate(jointPrefab, swordPosition.transform.position, Quaternion.identity);
        weapon.transform.SetParent(null);
        weapon.transform.position = swordPosition.transform.position;
        weapon.transform.eulerAngles = originalRotation+transform.eulerAngles;
        jointGO.transform.SetParent(transform);
        joint = jointGO.GetComponent<ConfigurableJoint>();
        jointGO.transform.eulerAngles = weapon.transform.eulerAngles;
        joint.connectedBody = rb;
        float currentTime = 0.0f;
        movingWrist = true;
        StartCoroutine(MoveWrist(jointGO));
        while(currentTime<attackTime){
            currentTime+=tickTime;
            yield return new WaitForSeconds(tickTime);
        }
        movingWrist = false;
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
        StartCoroutine("UnSwingSword");
    }
    IEnumerator MoveWrist(GameObject wrist){
        while(movingWrist){
            wrist.transform.eulerAngles+=Vector3.right*attackSpeed*Time.deltaTime;
            wrist.transform.position +=-Vector3.up*downMovementSpeed*Time.deltaTime;
            //wrist.transform.position += new Vector3(0,-1*Time.deltaTime,0);
            yield return null;
        }
        
    }
    IEnumerator UnSwingSword()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        Destroy(joint.gameObject);
        weapon.transform.SetParent(swordPosition.transform);
        //weapon.transform.position = originalOffset;
        weapon.transform.eulerAngles = originalRotation;
        attackCompleted = true;
        yield return null;
    }

}
