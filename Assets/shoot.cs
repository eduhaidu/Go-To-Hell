using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    public float damage=10f;
    public float range=100f;
    public int bulletCount=300;

    public Transform origin;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetMouseButton(0)){
            if(bulletCount>0){
                StartCoroutine(FireGun());
            }
            else{
                Debug.Log("Out of bullets");
            }
        }
    }

    IEnumerator FireGun(){
        RaycastHit hit;

        yield return new WaitForSeconds(0.3f);
            if(Physics.Raycast(origin.transform.position,origin.transform.forward, out hit, range)){
                Debug.Log(hit.transform.name);
                Debug.DrawLine(origin.transform.position,hit.point);
            }
            bulletCount--;
        yield return null;
        }


    }