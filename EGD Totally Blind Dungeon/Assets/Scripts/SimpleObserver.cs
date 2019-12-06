using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SimpleObserver : MonoBehaviour
{
    public abstract void OnNotify(NotificationType notice, string message, float value);
}
