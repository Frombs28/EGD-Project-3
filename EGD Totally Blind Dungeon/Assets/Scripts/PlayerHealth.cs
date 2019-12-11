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
    private float BPM = 0;
    private float BPS = 0;
    public float distoMax = 0.5f;
    private float distoLevel = 0;
    private float heartBeatinit = -18;
    public float heartBeatVol = -18;
    public float heartBeatVolChange = 6;
    public AudioMixer mixer;
    bool firstTime = true;
    TutorialManager tutMan;
    
    // Start is called before the first frame update
    void Start()
    {
        heartBeatinit = heartBeatVol;
        aud = GetComponent<AudioSource>();
        aud.clip = heartBeat;
        BPM = init_BPM;
        BPS = 60/BPM;
        tutMan = FindObjectOfType<TutorialManager>();
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

        if (health <= 0)
        {
            StopCoroutine("Heartbeat");
            BPM = init_BPM;
            BPS = 0;
            aud.clip = deathSFX;
            distoLevel = 0;
            mixer.SetFloat("MasterDisto", distoLevel);
            heartBeatVol = heartBeatinit;
            mixer.SetFloat("HeartbeatVolume", heartBeatinit);
            aud.Play();
            Death();
        }
        else
        {           
            distoLevel = Mathf.Lerp(distoMax, 0, health / MAX_HEALTH);
            print("disto incrase: " + distoLevel.ToString());
            mixer.SetFloat("MasterDisto", distoLevel);
            heartBeatVol += heartBeatVolChange;
            mixer.SetFloat("HeartbeatVolume", heartBeatVol);
            aud.clip = heartBeat;
            StopCoroutine("Heartbeat");
            BPM += 35;
            BPS = 60 / BPM;
            StartCoroutine("Heartbeat");
        }
    }

    public void AddHealth()
    {
        StopCoroutine("Heartbeat");
        health = MAX_HEALTH;
        BPM = init_BPM;
        BPS = 0;
        distoLevel = 0;
        mixer.SetFloat("MasterDisto", distoLevel);
        heartBeatVol = heartBeatinit;
        mixer.SetFloat("HeartbeatVolume", heartBeatinit);
        if (firstTime)
        {
            firstTime = false;
            tutMan.Step8();
        }
        //aud.clip = healing sfx plz;
        //aud.Play();
    }

    IEnumerator Heartbeat()
    {
        while (true)
        {
            yield return new WaitForSeconds(BPS);
            print("audioisplaying");
            aud.Play();
        }
    }

    public void Death()
    {
        leftHand.DropRespawn();
        rightHand.DropRespawn();
        it.PreLoad();
        health = MAX_HEALTH;
    }
}
