using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptBome : MonoBehaviour
{
    public AudioClip soundbomb;
    AudioSource sound;
    public float HP = 20;

    public GameObject EfBomb;
    public GameObject arbomb;



    public void TakeDamage(float damage)
    {
        HP -= damage;

        if (HP <= 0)
        {
            sound.PlayOneShot(soundbomb);
            arbomb.SetActive(true);
            Instantiate(EfBomb, transform.position, transform.rotation);

            Invoke("des", 1f);




        }
    }


    void Start()
    {
        arbomb.SetActive(false);
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void des()
    {
        arbomb.SetActive(false);

        Destroy(gameObject);


    }
}
