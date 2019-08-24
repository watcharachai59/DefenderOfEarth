using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookatPlayer : MonoBehaviour
{

    public Transform target;

    public static bool look;
    // Start is called before the first frame update
    void Start()
    {
        // target = GameObject.Find("Player").GetComponent<Transform>();


        look = true;


    }

    // Update is called once per frame
    void Update()
    {
        if (look == true)
        {
            transform.LookAt(target);
        }


    }
}
