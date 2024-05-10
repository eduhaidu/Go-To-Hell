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
    }

    void Update()
    {
        string key = "";  // Initialize key as an empty string

        switch (Input.GetKey(KeyCode.A) ? "A" : (Input.GetKey(KeyCode.D) ? "D" : (Input.GetKey(KeyCode.W) ? "W" : (Input.GetKey(KeyCode.S) ? "S" : ""))))
        {
            case "W":
                // Move forward logic
                isWalking = true;
                break;
            case "A":
                isWalking = true;
                // Move left logic
                break;
            case "S":
                isWalking = true;
                // Move backward logic (optional)
                break;
            case "D":
                isWalking = true;
                // Move right logic
                break;
            default:
                isWalking = false;
                // No keys pressed (optional)
                break;
        }

        if (isWalking && Grounded)
        {
            stepsound = true;
            StartCoroutine(ExecuteSteps());
        }
        else
        {
            stepsound = false;
            // Stop any currently running footstep coroutine
            if (isStepping)
            {
                StopCoroutine(ExecuteSteps());
                isStepping = false;
            }
        }
    }
        IEnumerator ExecuteSteps()
    {
        if (!isStepping) // Only play if not already playing
        {
            isStepping = true;
            yield return new WaitForSeconds(stepInterval);
            source.PlayOneShot(StepSound, 1.0f);
            isStepping = false;
        }
        yield return null;
    }
}

