using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;

public class pickup_handler : MonoBehaviour
{
    public Transform player;
    public GameObject weaponScript;
    public GameObject healthScript;
    public GameObject Player;
    public GameObject doorScript;
    public AudioClip healthPickupSound;
    public AudioClip bulletPickupSound;
    void Start()
    {
        Player=GameObject.Find("Q3Player");
        player = GameObject.Find("Q3Player").transform;
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other){
        if (other.tag=="BulletPickup"){
           var gunscr = weaponScript.GetComponent<shoot>();
            gunscr.bulletCount += other.gameObject.GetComponent<bullet_pickup>().bulletAmmount;
            AudioSource.PlayClipAtPoint(bulletPickupSound,player.position);
            Destroy(other.gameObject);
        }
        if(other.tag=="HealthPickup"){
            var hpscr=healthScript.GetComponent<player_health>();
            hpscr.playerHealth+=other.gameObject.GetComponent<health_pickup>().healthAmmount;
            AudioSource.PlayClipAtPoint(healthPickupSound,player.position);
            Destroy(other.gameObject);
        }

        if (other.tag == "RocketPickup")
        {
            weaponScript.GetComponent<shoot>().rocketCount++;
            Destroy(other.gameObject);
            print("picket up rocket");
            AudioSource.PlayClipAtPoint(bulletPickupSound, player.position);
        }
        if (other.tag == "Key")
        {
            var keyList = Player.GetComponent<KeyCollection>().keys;
            keyList.Add(other.gameObject.GetComponent<KeyClass>().keyID);
            Destroy(other.gameObject);
        }
        if (other.tag == "Door"){
            doorScript=other.gameObject;
            var door = doorScript.GetComponent<DoorScript>();
            if(Player.GetComponent<KeyCollection>().keys.Contains(door.RequiredKeyID)){
                Debug.Log("Door Opened");
                Destroy(other.gameObject);
            }
        }
    }
}
