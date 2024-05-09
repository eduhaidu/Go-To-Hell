using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_health : MonoBehaviour
{
    public int playerHealth=100;
    public Camera PlayerCam;
    public GameObject PlayerPrefab;

    public GameObject DeathCam;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DeathZone")
        {
            Instadeath();
        }
    }

    public void Instadeath()
    {
        playerHealth = 0;
        PlayerCam.enabled = false;
        Instantiate(DeathCam,PlayerCam.transform.position, Quaternion.identity);
        Destroy(PlayerPrefab);

    }
}