using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Bossfinal : MonoBehaviour
{
    public AudioClip soundwalk;
    AudioSource sound;


    float HP = 100000;
    public static Collider cd;

    public float distance;


    public Transform target;


    public static NavMeshAgent agent;
    public static Animator ani;
    public static Rigidbody rb;

    public static float HPLoseBack = 300;


    public void StartAgent()
    {
        print("OffAni");
        agent.enabled = true;
        Invoke("onagent", 1f);

        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
    }

    void onagent()
    {


        rb.isKinematic = false;
    }

    public void TakeDamage(float damage)
    {
        if (HP <= 0)
        {
            cd.enabled = false;
            ani.SetTrigger("die");
            agent.isStopped = true;
            agent.updatePosition = false;
            rb.velocity = Vector3.zero;
            agent.enabled = false;
            return;
        }
        if (HP < HPLoseBack)
        {
            // transform.LookAt(target);
            //  ani.SetTrigger("Damge");

            agent.enabled = false;
            //  rb.AddForce(-transform.forward * 120);

            HPLoseBack -= 300;
        }


        HP -= damage;

        //  agent.updatePosition = false;
        //   agent.isStopped = true;



    }

    public void BossWalk()
    {
        sound.PlayOneShot(soundwalk);
    }
    void Start()
    {

        sound = GetComponent<AudioSource>();

        //  target = GameObject.Find("Player").GetComponent<Transform>();
        HPLoseBack = 99700;
        rb = GetComponent<Rigidbody>();

        cd = GetComponent<Collider>();
        agent = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
    }

    void des()
    {
        Destroy(gameObject);
    }
    void Update()
    {
        if (ScriptHeadShot.isdead == true)
        {
            Invoke("des", 6f);
        }

        if (agent.enabled == true)
        {
            distance = Vector3.Distance(transform.position, target.position);
            if (distance > 3)
            {

                agent.destination = target.position;
                agent.isStopped = false;
                agent.updatePosition = true;
                //  agent.SetDestination(target.position);
                ani.SetBool("Run", true);
                ani.SetBool("atk", false);



            }
            else if (distance < 3)
            {
                // agent.enabled = true;
                agent.isStopped = true;
                // agent.updatePosition = false;
                ani.SetBool("Run", false);
                ani.SetBool("atk", true);

            }





        }


    }

    public void atk()
    {
        ScriptHPStam.currentHP -= 20;

        ScriptHPStam.damaged = true;
    }

    public void dam()
    {
        //transform.Translate(100 * Time.deltaTime, 0, 0, 0 );

        transform.Translate(-Vector3.forward * Time.deltaTime * 5f);


    }








}
