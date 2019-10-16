using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Interact : MonoBehaviour
{
    [HideInInspector]
    public Hand m_ActiveHand = null;
}
