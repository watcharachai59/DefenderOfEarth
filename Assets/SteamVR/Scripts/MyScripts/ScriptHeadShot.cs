using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ScriptHeadShot : MonoBehaviour
{
    int killboss;
    public AudioClip sounddie;
    AudioSource sound;
    float HP = 500;
    public Slider hpboss;

    public static bool checkwin;


    float HPLoseBack = 100;
    public static bool isdead;

    public void TakeDamage(float damage)
    {
        HP -= damage;
        if (HP <= 0)
        {

            sound.PlayOneShot(sounddie);
            isdead = true;
            Bossfinal.ani.SetBool("die", true);
            //  Bossfinal.agent.isStopped = true;
            Bossfinal.agent.updatePosition = false;
            Bossfinal.rb.velocity = Vector3.zero;
            Bossfinal.agent.enabled = false;
            LookatPlayer.look = false;



        }
        if (HP < HPLoseBack)
        {
            // transform.LookAt(target);
            Bossfinal.ani.SetTrigger("Damge");
            Bossfinal.agent.enabled = false;
            // Bossfinal.rb.AddForce(-transform.forward * 100f);
            Bossfinal damge = GameObject.Find("Bossfinal").GetComponent<Bossfinal>();
            damge.dam();



            Bossfinal.rb.isKinematic = false;




            HPLoseBack -= 100;
        }








    }




    void Start()
    {
        isdead = false;
        killboss = 0;
        checkwin = false;
        HPLoseBack = 400;
        sound = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (killboss >= 1)
        {
            checkwin = true;
        }


        hpboss.value = HP;
        if (isdead == true)
        {
            killboss++;
            Bossfinal.rb.isKinematic = true;
            ScriptHPStam.timerActive = false;
            Bossfinal.agent.enabled = false;


        }
        else
        {

        }
    }
}
