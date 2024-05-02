using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup_handler : MonoBehaviour
{
    public GameObject weaponScript;
    public GameObject pickupScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other){
        if (other.tag=="Pickup"){
           var gunscr = weaponScript.GetComponent<shoot>();
           var pickupscr=pickupScript.GetComponent<bullet_pickup>();
           gunscr.bulletCount+=pickupscr.bulletAmmount;
        }
    }
}
