using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerHealth : MonoBehaviour
{
    public float health = 3;
    public float MAX_HEALTH = 3;
    public ItemTracker it;
    public Hand leftHand;
    public Hand rightHand;
    bool invincible = false;
    float timer;
    public float invincibleTime = 1f;
    private AudioSource aud;
    public AudioClip heartBeat;
    public AudioClip deathSFX;
    public int init_BPM = 120;
    private int BPM = 0;
    private float BPS = 0;
    public float distoMax = 0.5f;
    private float distoLevel = 0;
    public AudioMixer mixer;
    
    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
        aud.clip = heartBeat;
        BPM = init_BPM;
        BPS = 60/BPM;
        //it = gameObject.GetComponent<ItemTracker>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (invincible)
        {
            timer += Time.deltaTime;
            if(timer >= invincibleTime)
            {
                invincible = false;
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        print("XXXXXXXXXXXXXXXXXXXX " + collision.gameObject.tag);
        if (collision.gameObject.tag == "Enemy Weapon")
        {
            AIController enemy = collision.gameObject.GetComponent<EnemyWeapon>().myEnemy.gameObject.GetComponent<AIController>();
            if(enemy.currentAttack != null && !enemy.currentAttack.attackCompleted && !invincible)
            {
                SubtractHealth(enemy.damage);
                invincible = true;
                timer = 0f;
            }
        }
    }

    public void SubtractHealth(float damage)
    {
        health -= damage;

        print("I've been injured! Ugh!");

        if (health <= 0)
        {
            CancelInvoke();
            BPM = init_BPM;
            BPS = 0;
            aud.clip = deathSFX;
            aud.Play();
            Death();
        }
        else
        {
            distoLevel = Mathf.Lerp(distoMax, 0, health / MAX_HEALTH);
            mixer.SetFloat("MasterDisto", distoLevel);
            aud.clip = heartBeat;
            CancelInvoke();
            BPM += 20;
            BPS = 60 / BPM;
            InvokeRepeating("Heartbeat", 0, BPS);
        }
    }

    public void Heartbeat()
    {
        aud.Play();
    }

    public void Death()
    {
        leftHand.DropRespawn();
        rightHand.DropRespawn();
        it.LoadPlayer();
        health = MAX_HEALTH;
    }
}
