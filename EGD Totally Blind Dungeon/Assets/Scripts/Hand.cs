using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Hand : MonoBehaviour
{

    public SteamVR_Action_Boolean m_GrabAction = null;
    public SteamVR_Action_Vibration m_VibrateAction = null;
    private SteamVR_Behaviour_Pose m_Pose = null;
    //private FixedJoint m_Joint = null;
    private Interact m_CurrentInteract = null;
    private List<Interact> m_ContactInteracts = new List<Interact>();
    private bool held;
    public Vector3 snapPositionOffset;
    public Vector3 snapRotationOffset;
    public GameObject swordWrist;
    GameObject currentWrist;
    public SteamVR_Input_Sources source;
    private bool touching_interactable_haptic;
    //public SteamVR_Action_Vibration vibrate = null;

    private void Awake()
    {
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
        //source = gameObject.GetComponent<SteamVR_Input_Sources>();
        //m_Joint = GetComponent<FixedJoint>();
        held = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Down
        if (m_GrabAction.GetStateDown(m_Pose.inputSource))
        {
            print(m_Pose.inputSource + " Trigger Down");
            if (!held)
            {
                Pickup();
                held = true;
            }
            else
            {
                Drop();
                held = false;
            }
        }

        //// Up
        //if (m_GrabAction.GetStateUp(m_Pose.inputSource))
        //{
        //    print(m_Pose.inputSource + " Trigger Up");
        //    Drop();
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Interact"))
        {
            return;
        }
        m_ContactInteracts.Add(other.gameObject.GetComponent<Interact>());
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Interact"))
        {
            return;
        }
        m_ContactInteracts.Remove(other.gameObject.GetComponent<Interact>());
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.CompareTag("Interact"))
        {
            return;
        }
        if (held)
        {
            return;
        }
        m_VibrateAction.Execute(0f, 0.1f, 150f, 0.5f, source);
    }

    public void Pickup()
    {
        // Get nearest
        m_CurrentInteract = GetNearestInteract();

        // Null check
        if (m_CurrentInteract == null)
        {
            return;
        }

        // Already held check; if something is held, drop it
        if (m_CurrentInteract.m_ActiveHand)
        {
            m_CurrentInteract.m_ActiveHand.Drop();
        }

        // Rotation
        m_CurrentInteract.transform.eulerAngles = new Vector3(0, 0, 0);

        // Position
        m_CurrentInteract.transform.position = transform.position;

        // Attach
        //Transform targetTrans = m_CurrentInteract.GetComponent<Transform>();
        Rigidbody targetBody = m_CurrentInteract.GetComponent<Rigidbody>();
        currentWrist = Instantiate(swordWrist);
        ConfigurableJoint cj = currentWrist.GetComponent<ConfigurableJoint>();
        cj.connectedBody = targetBody;
        Transform targetTrans = currentWrist.GetComponent<Transform>();
        targetTrans.SetParent(transform);
        
        targetTrans.localRotation = Quaternion.Euler(90f,360f,90f);
        //targetTrans.Rotate(snapRotationOffset);
        targetTrans.localPosition = new Vector3(0.037f,0,0.05f);
        //targetTrans.Translate(snapPositionOffset, Space.Self);

        //targetBody.useGravity = false;
        //targetBody.isKinematic = true;

        //-------------------------------------------------------------------
        //Rigidbody targetBody = m_CurrentInteract.GetComponent<Rigidbody>();
        //m_Joint.connectedBody = targetBody;


        // Set active hand
        m_CurrentInteract.m_ActiveHand = this;
    }

    public void Drop()
    {
        // Null check
        if (!m_CurrentInteract)
        {
            return;
        }

        // Apply velocity
        Rigidbody targetBody = m_CurrentInteract.GetComponent<Rigidbody>();
        targetBody.velocity = m_Pose.GetVelocity();
        targetBody.angularVelocity = m_Pose.GetAngularVelocity();
        //targetBody.useGravity = true;
        //targetBody.isKinematic = false;

        // Detach
        //Transform targetTrans = m_CurrentInteract.GetComponent<Transform>();
        //targetTrans.SetParent(null);
        Destroy(currentWrist);

        //-----------------------------------------------------------------------
        //m_Joint.connectedBody = null;


        // Clear
        m_CurrentInteract.m_ActiveHand = null;
        m_CurrentInteract = null;


    }

    private Interact GetNearestInteract()
    {
        Interact nearest = null;
        float minDist = float.MaxValue;
        float distance = 0.0f;
        foreach(Interact interactable in m_ContactInteracts)
        {
            distance = (interactable.transform.position - transform.position).sqrMagnitude;
            if(distance < minDist)
            {
                minDist = distance;
                nearest = interactable;
            }
        }
        return nearest;
    }



}
