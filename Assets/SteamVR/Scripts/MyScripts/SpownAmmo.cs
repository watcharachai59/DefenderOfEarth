using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpownAmmo : MonoBehaviour
{
    [SerializeField]
    GameObject AmmoPrefab;

    private GameObject has;
    void Start()
    {
        // GameObject Ammo = Instantiate(AmmoPrefab, transform.position, transform.rotation);
        //  Ammo.transform.SetParent(transform);



    }

    private void Update()
    {
        /* if(GetComponent<CheckOnAmmo>().OnGun == true)
         {
             GameObject Ammo = Instantiate(AmmoPrefab, transform.position, transform.rotation);
         }*/
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "MaxL")
        {

            if (other.GetComponent<CheckOnAmmo>().OnGun == false)
            {
                has = null;

                Invoke("SS", 0.23f);

            }

        }
        else
       if (other.gameObject.name == "MaxR")
        {
            if (other.GetComponent<CheckOnAmmo>().OnGun == false)
            {
                has = null;

                Invoke("SS", 0.23f);

            }


        }
    }


    void SS()
    {
        if (has == null)
        {

            GameObject Ammo = Instantiate(AmmoPrefab, transform.position, transform.rotation);
            Ammo.transform.SetParent(transform);
            has = Ammo;
        }

    }

}
