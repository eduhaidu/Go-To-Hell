using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpHeight = 2f; // Adjust as needed
    public float jumpDuration = 0.5f; // Adjust as needed

    private bool isJumping = false;
    private float jumpStartTime;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isJumping)
        {
            CharacterController characterController = other.GetComponent<CharacterController>();
            if (characterController != null)
            {
                isJumping = true;
                jumpStartTime = Time.time;
                StartCoroutine(Jump(characterController));
            }
            else
            {
                Debug.LogWarning("Player object does not have a CharacterController component.");
            }
        }
    }

    IEnumerator Jump(CharacterController characterController)
    {
        float jumpVelocity = Mathf.Sqrt(2 * Physics.gravity.magnitude * jumpHeight);
        while (isJumping && Time.time - jumpStartTime < jumpDuration)
        {
            float elapsedTime = Time.time - jumpStartTime;
            float normalizedTime = elapsedTime / jumpDuration;
            float verticalVelocity = jumpVelocity * (1 - 4 * (normalizedTime - 0.5f) * (normalizedTime - 0.5f));
            Vector3 moveDirection = Vector3.up * verticalVelocity * Time.deltaTime;
            characterController.Move(moveDirection);
            yield return null;
        }
        isJumping = false;
    }

}
