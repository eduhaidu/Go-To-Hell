using System.Collections.Generic;
using UnityEngine;

public class ManageModelPieces : MonoBehaviour
{
    public GameObject[] modelPieces;
    private Dictionary<Rigidbody, Collider[]> pieceColliders = new Dictionary<Rigidbody, Collider[]>();
    private Dictionary<Rigidbody, float> stationaryTimes = new Dictionary<Rigidbody, float>();
    public float requiredStationaryTime = 1f; // Time in seconds the object needs to be stationary before disabling

    void Start()
    {
        foreach (GameObject piece in modelPieces)
        {
            Rigidbody rb = piece.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Collider[] colliders = piece.GetComponents<Collider>();
                pieceColliders.Add(rb, colliders);
                stationaryTimes.Add(rb, 0f);
            }
        }
    }

    void FixedUpdate()
    {
        List<Rigidbody> toRemove = new List<Rigidbody>();

        foreach (var entry in pieceColliders)
        {
            Rigidbody rb = entry.Key;
            if (rb != null)
            {
                if (rb.velocity.magnitude < 0.01f && IsGrounded(rb))
                {
                    stationaryTimes[rb] += Time.fixedDeltaTime;

                    if (stationaryTimes[rb] >= requiredStationaryTime)
                    {
                        Collider[] colliders = entry.Value;
                        foreach (Collider c in colliders)
                        {
                            Destroy(c.GetComponent<Rigidbody>());
                            c.enabled = false;
                            Destroy(c);
                        }

                        toRemove.Add(rb);
                    }
                }
                else
                {
                    stationaryTimes[rb] = 0f;
                }
            }
        }

        foreach (Rigidbody rb in toRemove)
        {
            pieceColliders.Remove(rb);
            stationaryTimes.Remove(rb);
        }
    }

    bool IsGrounded(Rigidbody rb)
    {
        // Perform a raycast or other check to determine if the object is grounded
        // This is a simple example using a raycast
        RaycastHit hit;
        if (Physics.Raycast(rb.transform.position, Vector3.down, out hit, 1f))
        {
            return hit.collider != null;
        }
        return false;
    }
}
