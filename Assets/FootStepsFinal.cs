using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FootStepsFinal : MonoBehaviour
{
    public string[] validTags;
    public bool Grounded;

    public bool isWalking;

    public bool stepsound; //debug only

    private bool isStepping = false;

    public AudioSource source;

    public AudioClip StepSound;

    public float stepInterval = 0.5f;

    private void OnTriggerEnter(Collider other)
    {

        // Collision detected with a valid tag! Perform your desired actions here
        Debug.Log("Collision detected with tag: " + other.gameObject.tag);
        // ... (Your specific logic for handling the collision)
        Grounded = true;
        print("Walking on " + other.gameObject.tag);
    }

    private void OnTriggerExit(Collider other)
    {
        Grounded = false;
        isWalking = false;
    }

    void Update()
    {
        bool wasWalking = isWalking;
        if(Grounded){
            isWalking = Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.S);
        }
        else{
            isWalking = false;
        }
        if (isWalking && Grounded && !wasWalking){
            stepsound=true;
            StartCoroutine(ExecuteSteps());
        }
        else if (!isWalking&&isStepping){
            stepsound = false;
            //Stop any currently running footstep coroutine
            StopCoroutine(ExecuteSteps());
            isStepping = false;
        }
    }

    IEnumerator ExecuteSteps(){
        while(isWalking&&Grounded){
            if(!isStepping){
                isStepping = true;
                source.PlayOneShot(StepSound,1.0f);
                yield return new WaitForSeconds(stepInterval);
                isStepping = false;
            }
            else{
                yield return null;
            }
        }
    }
}

