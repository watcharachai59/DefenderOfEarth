using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OpenDoor : MonoBehaviour
{
    public static bool checkopen;
    float HP = 10;
    public void TakeDamage(float damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            checkopen = true;

        }


    }
    void Start()
    {
        checkopen = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
