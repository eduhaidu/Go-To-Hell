using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_health : MonoBehaviour
{
    public int playerHealth=100;
    public Camera PlayerCam;
    public GameObject PlayerPrefab;

    public GameObject DeathCam;

    public ParticleSystem PlayerGoreBlood;
    public GameObject PlayerGoreModel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DeathZone")
        {
            PlayerDeath();
        }
    }

    public void PlayerDeath()
    {
        playerHealth = 0;
        PlayerCam.enabled = false;
        Instantiate(DeathCam, PlayerCam.transform.position, Quaternion.identity);
        Instantiate(PlayerGoreBlood,PlayerCam.transform.position, Quaternion.identity);
        Instantiate(PlayerGoreModel, PlayerCam.transform.position, Quaternion.identity);

        Destroy(PlayerPrefab);
    }
}
