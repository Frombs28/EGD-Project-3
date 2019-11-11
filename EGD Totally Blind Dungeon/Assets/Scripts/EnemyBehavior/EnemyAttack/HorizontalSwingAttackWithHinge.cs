using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalSwingAttackWithHinge : EnemyAttack
{
    public float angle = 45f;
    public float attackSpeed = 45f;
    public float attackTime = 2f;
    public float tickTime = .2f;
    public float sideMovementSpeed = .5f;
    public float direction = 1f;
    Vector3 originalOffset;
    Vector3 originalRotation;
    public GameObject objectWithMaterial = null;
    public GameObject jointPrefab;
    public GameObject swordPosition;
    Rigidbody rb;
    Rigidbody enemyrb;
    ConfigurableJoint joint;
    bool movingWrist = false;
    bool interrupted;

    public float currentTime;
    //Vector3 originalScale;
    private void Start() {
        originalOffset = weapon.transform.localPosition;
        originalRotation = weapon.transform.eulerAngles;
        rb = weapon.GetComponent<Rigidbody>();
        enemyrb = GetComponent<Rigidbody>();
        currentTime = 0f;
        //originalScale = weapon.transform.localScale;
        //StartAttack();
    }
    private void Update()
    {
        if(!movingWrist) weapon.transform.localPosition = originalOffset;
        //print(parryable);
        //weapon.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //Debug.Log(weapon.GetComponent<AudioSource>().isPlaying);
    }
    public override void StartAttack(){
        AudioSource audio = weapon.GetComponent<AudioSource>();
        if(audio!=null)audio.Play();
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
        movingWrist = true;
        StartCoroutine(SwingSwordParry());
        StartCoroutine(MoveWrist(jointGO));
        while(currentTime<attackTime){
            currentTime+=tickTime;
            yield return new WaitForSeconds(tickTime);
        }
        movingWrist = false;
        StartCoroutine("UnSwingSword");
    }
    IEnumerator MoveWrist(GameObject wrist){
        wrist.transform.localEulerAngles=new Vector3(0,0,0);
        weapon.transform.localEulerAngles = new Vector3(0,-direction*attackSpeed/2*attackTime+weapon.transform.localEulerAngles.y,0);
        while(movingWrist){
            weapon.transform.localEulerAngles += new Vector3(0,direction*attackSpeed*Time.deltaTime,0);
            wrist.transform.position +=direction*transform.right*sideMovementSpeed*Time.deltaTime;
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
        parryFrame = 29;    // 34-35 frames; 34 - parryFrame = frames that it is parryable
        frameCount = 0;
        parryable = false;
        attackCompleted = false;
        if (objectWithMaterial!=null) objectWithMaterial.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        float currentAngle = angle;
        float startTime = Time.realtimeSinceStartup;
        while(attackTime > currentTime){
            frameCount++;
            //currentTime += Time.deltaTime;
            float curTime = Time.realtimeSinceStartup;
            if (frameCount >= parryFrame && !parryable)
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
