using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalSwingAttackWithHinge : EnemyAttack
{
    public float angle = 45f;
    public float attackSpeed = 100f;
    public float preSwingTime = .6f;
    public float preSwingSpeed = 40f;
    public float preSwingMovementSpeed = .5f;
    public float attackTime = 2f;
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
    public bool interrupted;
    bool swingingUp = false;
    public float parryTime = 0.5f;
    private float parryableTimer = 0f;
    public AIController ai;

    private AudioSource aud;

    public float currentTime;
    //Vector3 originalScale;
    private void Start() {
        originalOffset = weapon.transform.localPosition;
        originalRotation = weapon.transform.eulerAngles;
        rb = weapon.GetComponent<Rigidbody>();
        enemyrb = GetComponent<Rigidbody>();
        currentTime = 0f;
        aud = weapon.GetComponent<AudioSource>();
        //originalScale = weapon.transform.localScale;
        //StartAttack();
    }
    private void Update()
    {
        if(!movingWrist&&!swingingUp) weapon.transform.localPosition = originalOffset;
        //print(parryable);
        //weapon.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //Debug.Log(weapon.GetComponent<AudioSource>().isPlaying);
    }
    public override void StartAttack(){
        attackCompleted = false;
        interrupted = false;
        objectWithMaterial.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        //enemyrb.constraints |= RigidbodyConstraints.FreezeRotationY;
        //weapon.transform.rotation = transform.rotation;
        weapon.transform.localPosition = originalOffset;
        //if(!startInFront) weapon.transform.RotateAround(transform.position, transform.right, angle*direction*-1);
        //weapon.GetComponent<BoxCollider>().enabled = true;
        StartCoroutine("SwingSword");
    }
    public override void InterruptAttack(){
        if (interrupted)
        {
            return;
        }
        interrupted = true;
        parryable = false;
        StopAllCoroutines();
        //weapon.GetComponent<BoxCollider>().enabled = false;
        objectWithMaterial.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        movingWrist = false;
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
        swingingUp = true;
        if (aud != null) aud.Play();
        ai.hitBoi = false;
        StartCoroutine(PreSwing(jointGO));
        while(currentTime<preSwingTime){
            currentTime+=Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        swingingUp = false;
        movingWrist = true;
        currentTime = 0.0f;
        StartCoroutine(SwingSwordParry());
        StartCoroutine(MoveWrist(jointGO));
        while(currentTime<attackTime){
            currentTime+= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        movingWrist = false;
        StartCoroutine("UnSwingSword");
    }
    IEnumerator PreSwing(GameObject wrist){
      while(swingingUp){
          Debug.Log("swinging up!!");
           wrist.transform.localEulerAngles+=Vector3.forward*preSwingSpeed*Time.deltaTime;
            wrist.transform.position +=Vector3.up*preSwingMovementSpeed*Time.deltaTime;
            //wrist.transform.position += new Vector3(0,-1*Time.deltaTime,0);
            yield return null;
        }  
    }
    IEnumerator MoveWrist(GameObject wrist){
        while(movingWrist){
            wrist.transform.localEulerAngles+=Vector3.forward*attackSpeed*-Time.deltaTime;
            wrist.transform.position +=-Vector3.up*downMovementSpeed*Time.deltaTime;
            //wrist.transform.position += new Vector3(0,-1*Time.deltaTime,0);
            yield return null;
        }
        
    }
    IEnumerator UnSwingSword()
    {
        //Debug.Log("Who called me?");
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        if(joint != null)Destroy(joint.gameObject);
        weapon.transform.SetParent(swordPosition.transform);
        weapon.transform.localPosition = originalOffset;
        weapon.transform.eulerAngles = originalRotation;
        yield return new WaitForSeconds(3);
        attackCompleted = true;
        if(objectWithMaterial!=null){
            //objectWithMaterial.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        }
        yield return null;
    }

    IEnumerator SwingSwordParry(){
        //frameCount = 0;
        parryable = false;
        attackCompleted = false;
        if (objectWithMaterial!=null) objectWithMaterial.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        float currentAngle = angle;
        float startTime = Time.realtimeSinceStartup;
        parryableTimer = currentTime;
        while(attackTime > currentTime){
            //frameCount++;
            //currentTime += Time.deltaTime;
            //float curTime = Time.realtimeSinceStartup;
            parryableTimer += Time.deltaTime;
            if (parryableTimer >= parryTime && !parryable)
            {
                parryable = true;
                if (objectWithMaterial!=null) objectWithMaterial.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
            }
            //weapon.transform.RotateAround(transform.position, transform.right * -1, attackSpeed*direction*Time.deltaTime);
            //currentAngle-=attackSpeed*Time.deltaTime;
            //weapon.transform.localScale = originalScale;
            yield return null;
        }
        currentTime = 0f;
        parryable = false;
        if(objectWithMaterial!=null){
            objectWithMaterial.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        }
    }

}
