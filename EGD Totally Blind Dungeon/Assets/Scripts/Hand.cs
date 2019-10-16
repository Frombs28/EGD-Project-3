using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Hand : MonoBehaviour
{

    public SteamVR_Action_Boolean m_GrabAction = null;
    private SteamVR_Behaviour_Pose m_Pose = null;
    private FixedJoint m_Joint = null;
    private Interact m_CurrentInteract = null;
    private List<Interact> m_ContactInteracts = new List<Interact>();

    private void Awake()
    {
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
        m_Joint = GetComponent<FixedJoint>();
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
            Pickup();
        }

        // Up
        if (m_GrabAction.GetStateDown(m_Pose.inputSource))
        {
            print(m_Pose.inputSource + " Trigger Down");
            Pickup();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    public void Pickup()
    {

    }

    public void Drop()
    {

    }

    private Interact GetNearestInteract()
    {
        return null;
    }
}
