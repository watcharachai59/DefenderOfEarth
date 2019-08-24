using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMax : MonoBehaviour
{
    public bool HasUse = false;

    // Update is called once per frame
    void Start()
    {
        HasUse = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!HasUse)
        {
            if (other.gameObject.name == "MaxR")
            {
                if (other.GetComponent<CheckOnAmmo>().OnGun == false)
                {
                    GetComponent<Rigidbody>().isKinematic = true;
                    transform.SetParent(other.transform);
                    transform.position = other.transform.position;
                    transform.rotation = other.transform.rotation;
                    other.GetComponent<CheckOnAmmo>().OnGun = true;
                    ScriptPlayer.MyInstance.GetMax(this.GetComponent<Rigidbody>(), true);

                    HasUse = true;


                }


            }
            if (other.gameObject.name == "MaxL")
            {
                if (other.GetComponent<CheckOnAmmo>().OnGun == false)
                {
                    GetComponent<Rigidbody>().isKinematic = true;
                    transform.SetParent(other.transform);
                    transform.position = other.transform.position;
                    transform.rotation = other.transform.rotation;
                    other.GetComponent<CheckOnAmmo>().OnGun = true;
                    ScriptPlayer.MyInstance.GetMax(this.GetComponent<Rigidbody>(), false);
                    HasUse = true;
                }
            }






        }

    }

}
