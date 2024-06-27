using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using EZCameraShake;

public class CrumblingPlatform : MonoBehaviour
{
    //Camera shake values
    public float timeBeforeCrumble=2.0f;
    public float shakeStrenght=0.5f;
    public GameObject[] PlatformPieces;
    void OnTriggerEnter(Collider other){
        if(other.tag=="Player"){
            
            CameraShaker.Instance.ShakeOnce(6f,4f,.1f,2f);
            StartCoroutine(CrumblePlatform());
        }
    }

    void OnTriggerExit(Collider other){
        if(other.tag=="Player"){
            CameraShake.StopShake();
        }
    }

    IEnumerator CrumblePlatform(){
        yield return new WaitForSeconds(timeBeforeCrumble);
        PlatformPieces[0].SetActive(false);
        this.gameObject.GetComponent<BoxCollider>().enabled=false;
        for (int i=1;i<PlatformPieces.Length;i++){
            PlatformPieces[i].SetActive(true);
        }
    }
}
