using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAmmo : MonoBehaviour
{

    private GameObject has;

    public GameObject player;

    public Transform target;

    void LateUpdate()
    {

        transform.position = player.transform.position;


        Vector3 targetPostition = new Vector3(target.position.x, this.transform.position.y, target.position.z);

        transform.LookAt(targetPostition);
    }
}
