using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class godoorboss : MonoBehaviour
{
    public GameObject spawnboss1;

    Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        spawnboss1.SetActive(false);

        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ScriptPlayer.checkstate == 34)
        {
            ani.SetBool("doorboss", true);




        }
    }

    void spawnboss()
    {
        spawnboss1.SetActive(true);
    }
}
