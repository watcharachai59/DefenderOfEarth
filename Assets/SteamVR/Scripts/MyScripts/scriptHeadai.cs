using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptHeadai : MonoBehaviour
{
    public int HP;

    private static scriptHeadai instance;

    public static scriptHeadai MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<scriptHeadai>();
            }

            return instance;
        }
    }
    public void TakeDamage(float damage)
    {
       // hit.Getcomponent<EnemyShooter>().Takedamage(100);
    }
}
