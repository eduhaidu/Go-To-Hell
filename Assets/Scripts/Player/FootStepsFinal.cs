using System.Collections;
using System.Linq;
using UnityEngine;

public class FootStepsFinal : MonoBehaviour
{
    public string[] validTags;
    public bool Grounded;
    public bool isWalking;
    public bool stepsound; // Debug only

    private bool isStepping = false;
    public AudioSource source;
    public AudioClip StepSound;
    public float stepInterval = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (validTags.Contains(other.gameObject.tag))
        {
            Debug.Log("Collision detected with tag: " + other.gameObject.tag);
            Grounded = true;
            print("Walking on " + other.gameObject.tag);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (validTags.Contains(other.gameObject.tag))
        {
            Grounded = false;
            isWalking = false;
        }
    }

    void Update()
    {
        bool wasWalking = isWalking;
        if (Grounded)
        {
            if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)){
                isWalking=true;
            }
            else{
                isWalking=false;
            }
            if((Input.GetKey(KeyCode.A)&&Input.GetKey(KeyCode.D))||(Input.GetKey(KeyCode.W)&&Input.GetKey(KeyCode.S))){
                isWalking=false;
            }
        }
        else
        {
            isWalking = false;
        }

        if (isWalking && Grounded && !wasWalking)
        {
            stepsound = true;
            if (!isStepping)
            {
                StartCoroutine(ExecuteSteps());
            }
        }
        else if (!isWalking && isStepping)
        {
            stepsound = false;
        }
    }

    IEnumerator ExecuteSteps()
    {
        isStepping = true;
        while (isWalking && Grounded)
        {
            source.PlayOneShot(StepSound, 1.0f);
            yield return new WaitForSeconds(stepInterval);
        }
        isStepping = false;
    }
}