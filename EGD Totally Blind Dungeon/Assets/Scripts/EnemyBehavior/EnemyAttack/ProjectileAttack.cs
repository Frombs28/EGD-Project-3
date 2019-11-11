using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : EnemyAttack
{
    public GameObject projectile;
    public Transform projectileSpawnLocation;
    public float timeToAttack = 2f;
    public float attackActiveTime = 2f;
    GameObject currentProjectile = null;
    void Start(){
    }
    public override void StartAttack(){
        StartCoroutine("SpawnProjectile");
        attackCompleted = false;
    }
    public override void InterruptAttack(){
        StopAllCoroutines();
        if(currentProjectile!=null) Destroy(currentProjectile);
        currentProjectile = null;
        attackCompleted = true;
    }
    IEnumerator SpawnProjectile(){
        //maybe switch below to a while loop for sound timing
        yield return new WaitForSeconds(timeToAttack);
        currentProjectile = Instantiate(projectile, projectileSpawnLocation.position, Quaternion.identity);
        currentProjectile.transform.localEulerAngles = projectileSpawnLocation.localEulerAngles;
        //possibily have the movement script for the projectile on itself rather than here
        yield return new WaitForSeconds(attackActiveTime);
        Destroy(currentProjectile);
        currentProjectile = null;
        attackCompleted = true;
    }
}
