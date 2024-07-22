using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScriptLever : MonoBehaviour
{
    public GameObject[] RequiredLevers;
    public bool canOpen = false;

    // Update is called once per frame
    void Update()
    {
        for(int i=0; i<RequiredLevers.Length; i++){
            if(RequiredLevers[i].GetComponent<LeverScript>().isActivated == false){
                canOpen=false;
                break;
            }
            else{
                canOpen=true;
            }
        }
        if (canOpen){
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y+5,transform.position.z),Time.deltaTime);
        }
    }
}
