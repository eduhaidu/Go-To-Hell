using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimHandler : MonoBehaviour
{

    public Animator WeaponAnimator;
    public bool isWalking;
    public string AnimaBoolTrigger="isWalking";
    public GameObject MultiGun;

    public void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
        if((Input.GetKey(KeyCode.A)&&Input.GetKey(KeyCode.D))||(Input.GetKey(KeyCode.W)&&Input.GetKey(KeyCode.S))){
            
            isWalking=false;
        }

        if(isWalking ) { MultiGun.GetComponent<Animator>().SetBool(AnimaBoolTrigger, true); } else { MultiGun.GetComponent<Animator>().SetBool(AnimaBoolTrigger, false); }
    }
}
