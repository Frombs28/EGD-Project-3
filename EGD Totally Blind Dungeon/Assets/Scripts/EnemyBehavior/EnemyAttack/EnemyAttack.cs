using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour
{
    public bool parryable = false;
    public float frameCount = 0;
    public float parryFrame = 0.5f;

    public GameObject weapon = null;
    public bool attackCompleted = false;
    public abstract void StartAttack();
    public abstract void InterruptAttack();
    public virtual bool IsAttackDone(){return attackCompleted;}  
}
