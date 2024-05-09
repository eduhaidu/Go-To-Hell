using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSystem : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip grass;
    public AudioClip concrete;
    public AudioClip wood;
    public AudioClip dirt;

    RaycastHit hit;
    public Transform RayStart;
    public float range;
    public LayerMask layerMask;
    // Start is called before the first frame update
    public IEnumerator checkFootstepTag(){
        if(Physics.Raycast(RayStart.position,-RayStart.transform.up,out hit, range, layerMask)){
            if(hit.collider.CompareTag("grass")) PlayFootstepSound(grass);
            if(hit.collider.CompareTag("concrete")) PlayFootstepSound(concrete);
            if(hit.collider.CompareTag("wood")) PlayFootstepSound(wood);
            if(hit.collider.CompareTag("dirt")) PlayFootstepSound(dirt);
            yield return new WaitForSeconds(.1f);
        }
    }

    void PlayFootstepSound(AudioClip floorType){
        audioSource.pitch = Random.Range(0.8f,1f);
        audioSource.PlayOneShot(floorType);
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W)) StartCoroutine(checkFootstepTag());
        if(Input.GetKey(KeyCode.A)) StartCoroutine(checkFootstepTag());
        if(Input.GetKey(KeyCode.S)) StartCoroutine(checkFootstepTag());
        if(Input.GetKey(KeyCode.D)) StartCoroutine(checkFootstepTag());
        Debug.DrawRay(RayStart.position, -RayStart.transform.up*range, Color.green);
    }
}
