using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RLauncher : MonoBehaviour
{
    public GameObject RocketPrefab;
    public float RocketSpeed = 10.0f;
    public int RocketCount;
    public Transform RocketSpawn;
    public Transform RocketOrientation;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            instantiateRocket();
        }
    }

    public void instantiateRocket()
    {
        var rocket = Instantiate(RocketPrefab, RocketSpawn.position,Quaternion.identity);

        rocket.transform.forward = RocketSpawn.forward; // Set forward direction (optional)
        rocket.GetComponent<Rigidbody>().velocity = rocket.transform.forward * RocketSpeed;
    }
}
