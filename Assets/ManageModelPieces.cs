using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageModelPieces : MonoBehaviour
{
    public GameObject[] modelPieces;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
        for(int i=0;i<modelPieces.Length;i++){
            if(modelPieces[i].GetComponent<Rigidbody>().velocity.magnitude<=0.1){
                foreach(Collider c in GetComponents<Collider>()){
                    c.enabled=false;
                }
            }
            }
        }
}

