using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public GameObject myEnemy;
    // Start is called before the first frame update
    void Start()
    {
        myEnemy = transform.parent.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
