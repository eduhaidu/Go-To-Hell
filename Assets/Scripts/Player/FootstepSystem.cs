using System.Collections;
using UnityEngine;

public class FootstepSystem : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip grass;
    public AudioClip concrete;
    public AudioClip wood;
    public AudioClip dirt;

    public Transform rayStart;
    public float range;
    public LayerMask layerMask;

    private bool isMoving;
    private bool isPlayingFootstep;

    private void Update()
    {
        CheckMovementInput();
        Debug.DrawRay(rayStart.position, -rayStart.transform.up * range, Color.green);
    }

    private void CheckMovementInput()
    {
        if (!isMoving &&
            (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) ||
             Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)))
        {
            isMoving = true;
            StartCoroutine(CheckFootstepTag());
        }
        else if (isMoving &&
                 (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) ||
                  Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D)))
        {
            isMoving = false;
            audioSource.Stop(); // Stop footstep sound when the player stops moving
        }
    }

    private IEnumerator CheckFootstepTag()
    {
        isPlayingFootstep = true;
        yield return new WaitForSeconds(0.1f); // Delay to ensure the player is moving

        while (isMoving)
        {
            RaycastHit hit;
            if (Physics.Raycast(rayStart.position, -rayStart.transform.up, out hit, range, layerMask))
            {
                AudioClip footstepSound = null;
                switch (hit.collider.tag)
                {
                    case "concrete":
                        footstepSound = concrete;
                        break;
                    case "grass":
                        footstepSound = grass;
                        break;
                    case "wood":
                        footstepSound = wood;
                        break;
                    case "dirt":
                        footstepSound = dirt;
                        break;
                }

                if (footstepSound != null)
                    PlayFootstepSound(footstepSound);
            }
            else{
                audioSource.Stop();
            }

            yield return new WaitForSeconds(1f); // Adjust as needed
        }

        isPlayingFootstep = false;
    }

    private void PlayFootstepSound(AudioClip floorType)
    {
        audioSource.pitch = Random.Range(0.8f, 1f);
        audioSource.PlayOneShot(floorType);
    }
}