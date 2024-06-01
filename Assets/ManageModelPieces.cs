using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageModelPieces : MonoBehaviour
{
    public GameObject[] modelPieces;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < modelPieces.Length; i++)
        {
            Rigidbody rb = modelPieces[i].GetComponent<Rigidbody>();
            if (rb != null && rb.velocity.magnitude <= 0.1f)
            {
                Collider[] colliders = modelPieces[i].GetComponents<Collider>();
                foreach (Collider c in colliders)
                {
                    Destroy(c.GetComponent<Rigidbody>());
                    c.enabled = false;
                    Destroy(c);
                }
            }
        }
    }
}
