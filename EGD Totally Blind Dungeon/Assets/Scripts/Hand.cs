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
    public float amplitude = 0.1f;
    public float frequency = 20f;
    public int handIndex = 0;    // Determines which hand this is: 0 is left, 1 is right
    public ItemTracker it;
    float curSavingTime = 0;
    float maxSavingTime = 5f;
    bool haveSaved = false;
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
        it = GameObject.FindGameObjectWithTag("Player").GetComponent<ItemTracker>();
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
            }
            else
            {
                Drop();
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
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            curSavingTime = 0f;
            return;
        }
        if (!other.gameObject.CompareTag("Interact"))
        {
            return;
        }
        if (other.gameObject.transform.parent != null)
        {
            m_ContactInteracts.Add(other.gameObject.transform.parent.gameObject.GetComponent<Interact>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            haveSaved = false;
            return;
        }
        if (!other.gameObject.CompareTag("Interact"))
        {
            return;
        }
        if (other.gameObject.transform.parent != null)
        {
            m_ContactInteracts.Remove(other.gameObject.transform.parent.GetComponent<Interact>());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Checkpoint")&&!haveSaved)
        {
            curSavingTime += Time.deltaTime;
            if(curSavingTime >= maxSavingTime)
            {
                it.SavePlayer();
                haveSaved = true;
                return;
            }
        }
        if (!other.gameObject.CompareTag("Interact"))
        {
            return;
        }
        if (held)
        {
            return;
        }
        if (other.gameObject.GetComponent<Interact>().m_ActiveHand != null)
        {
            return;
        }
        m_VibrateAction.Execute(0f, 0.1f, frequency, amplitude, source);
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

        // Set held equal to true
        held = true;

        // Save that this item is in this hand
        if(handIndex == 0)
        {
            it.NewLeftHandItem(m_CurrentInteract);
        }
        else
        {
            it.NewRightHandItem(m_CurrentInteract);
        }
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

        // Set held to false
        held = false;

        // Save that this item is removed from this hand
        if (handIndex == 0)
        {
            it.RemoveLeftHandItem();
        }
        else
        {
            it.RemoveRightHandItem();
        }

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

    public void spawningItem(Interact item)
    {
        // this may or may not be necessary // m_ContactInteracts.Add(item.gameObject.GetComponent<Interact>());
        m_CurrentInteract = item;

        // Null check
        if (m_CurrentInteract == null)
        {
            return;
        }

        // Rotation
        m_CurrentInteract.transform.eulerAngles = new Vector3(0, 0, 0);

        // Position
        m_CurrentInteract.transform.position = transform.position;

        // Attach
        Rigidbody targetBody = m_CurrentInteract.GetComponent<Rigidbody>();
        currentWrist = Instantiate(swordWrist);
        ConfigurableJoint cj = currentWrist.GetComponent<ConfigurableJoint>();
        cj.connectedBody = targetBody;
        Transform targetTrans = currentWrist.GetComponent<Transform>();
        targetTrans.SetParent(transform);
        targetTrans.localRotation = Quaternion.Euler(90f, 360f, 90f);
        targetTrans.localPosition = new Vector3(0.037f, 0, 0.05f);

        // Set active hand
        m_CurrentInteract.m_ActiveHand = this;

        // Set held equal to true
        held = true;

        // Save that this item is in this hand
        if (handIndex == 0)
        {
            it.NewLeftHandItem(m_CurrentInteract);
        }
        else
        {
            it.NewRightHandItem(m_CurrentInteract);
        }

    }



}
