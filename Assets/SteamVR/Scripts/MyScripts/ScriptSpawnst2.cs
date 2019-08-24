using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptSpawnst2 : MonoBehaviour
{
    public GameObject st2;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void spawns2()
    {
        Instantiate(st2, transform.position, transform.rotation);
    }
}
