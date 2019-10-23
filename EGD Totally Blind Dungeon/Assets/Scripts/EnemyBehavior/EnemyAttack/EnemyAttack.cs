using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour
{
    public bool parryable = false;
    public int frameCount = 0;
    public int parryFrame = 4;

    public GameObject weapon = null;
    public bool attackCompleted = false;
    public abstract void StartAttack();
    public abstract void InterruptAttack();
    public virtual bool IsAttackDone(){return attackCompleted;}  
}
