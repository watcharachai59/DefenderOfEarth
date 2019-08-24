using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reHp : MonoBehaviour
{
    public float HP = 1;


    public void TakeDamage(float damage)
    {
        HP -= damage;

        if (HP <= 0)
        {

            Destroy(gameObject);
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(Time.deltaTime * 0, 2, 0));

    }
}
