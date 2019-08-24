using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptSolider : MonoBehaviour
{
    float aowailop;
    float ti = 0.1f;

    float Lookspeed = 1f;

    Transform targets;
    

    Transform Target;

    Vector3 target;

    public int GoStand;

    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private Transform Gun;

    void Start()
    {

        Target = GameObject.Find("Camera").GetComponent<Transform>();


    }


    void Update()
    {
        // transform.LookAt(target, Vector3.up);
        // transform.position = Vector3.Lerp(transform.position, target.position, 0.01f);
        transform.LookAt(Target);

        



        
            
       
    }










}
