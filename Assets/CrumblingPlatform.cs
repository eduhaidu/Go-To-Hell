using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CrumblingPlatform : MonoBehaviour
{
    //Camera shake values
    public float timeBeforeCrumble=2.0f;
    public float shakeStrenght=0.5f;
    public GameObject[] PlatformPieces;
    void OnTriggerEnter(Collider other){
        if(other.tag=="Player"){
            CameraShake.Shake(timeBeforeCrumble,shakeStrenght);
            StartCoroutine(CrumblePlatform());
        }
    }

    IEnumerator CrumblePlatform(){
        yield return new WaitForSeconds(timeBeforeCrumble);
        PlatformPieces[0].SetActive(false);
        for (int i=1;i<PlatformPieces.Length;i++){
            PlatformPieces[i].SetActive(true);
        }
    }
}
