using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

// Dont be a mean sausage

public class StickManipulation : MonoBehaviour
{
    public float m_Sensitivity = 0.1f;
    public float m_MaxSpeed = 1.0f;
    public float m_DeadZone = 0.1f;
    public SteamVR_Action_Boolean m_MovePress = null;
    public SteamVR_Action_Vector2 m_MoveValue = null;

    private float m_SpeedY = 0.0f;
    private float m_SpeedX = 0.0f;

    private CharacterController m_CharacterController = null;
    private Transform m_CameraRig = null;
    private Transform m_Head = null;

    private void Awake()
    {
        m_CharacterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        m_CameraRig = SteamVR_Render.Top().origin;
        m_Head = SteamVR_Render.Top().head;
    }

    private void Update()
    {
        HandleHead();
        HandleHeight();
        CalculateMovement();
    }

    private void HandleHead()
    {
        // Store
        Vector3 oldPos = m_CameraRig.position;
        Quaternion oldRot = m_CameraRig.rotation;

        // Rotate
        transform.eulerAngles = new Vector3(0.0f, m_Head.rotation.eulerAngles.y, 0.0f);

        // Restore
        m_CameraRig.position = oldPos;
        m_CameraRig.rotation = oldRot;

    }

    private void CalculateMovement()
    {
        // Orient Movement
        Vector3 orientationEuler = new Vector3(0, transform.eulerAngles.y, 0);
        Quaternion orientation = Quaternion.Euler(orientationEuler);
        Vector3 movement = Vector3.zero;

        

        // If button pressed
        //print(m_MoveValue.axis.x);
        //print(m_MoveValue.axis.y);

        if (Mathf.Abs(m_MoveValue.axis.y) > m_DeadZone && m_MovePress.state == true)
        {
            m_SpeedY += m_MoveValue.axis.y * m_Sensitivity;
            m_SpeedY = Mathf.Clamp(m_SpeedY, -m_MaxSpeed, m_MaxSpeed);
            movement += orientation * (m_SpeedY * Vector3.forward) * Time.deltaTime;
        }

        else
        {
            m_SpeedY = 0;
        }

        if (Mathf.Abs(m_MoveValue.axis.x) > m_DeadZone && m_MovePress.state == true)
        {
            m_SpeedX += m_MoveValue.axis.x * m_Sensitivity;
            m_SpeedX = Mathf.Clamp(m_SpeedX, -m_MaxSpeed, m_MaxSpeed);
            movement += orientation * (m_SpeedX * Vector3.right) * Time.deltaTime;
        }

        else
        {
            m_SpeedX = 0;
        }

        // Apply
        m_CharacterController.Move(movement);
    }

    private void HandleHeight()
    {
        // Get the head
        float headHeight = Mathf.Clamp(m_Head.localPosition.y, 1, 2);
        m_CharacterController.height = headHeight;

        // Cut in half
        Vector3 newCenter = Vector3.zero;
        newCenter.y = m_CharacterController.height / 2;
        newCenter.y += m_CharacterController.skinWidth;

        // Move capsule in local space
        newCenter.x = m_Head.localPosition.x;
        newCenter.z = m_Head.localPosition.z;

        // Rotate
        newCenter = Quaternion.Euler(0, -transform.eulerAngles.y, 0) * newCenter;

        // Apply
        m_CharacterController.center = newCenter;
    }

}