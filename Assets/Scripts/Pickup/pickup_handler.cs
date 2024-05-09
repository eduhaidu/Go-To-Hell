using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup_handler : MonoBehaviour
{
    public Transform player;
    public GameObject weaponScript;
    public GameObject healthScript;
    // Start is called before the first frame update
    public AudioClip healthPickupSound;
    public AudioClip bulletPickupSound;
    void Start()
    {
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
    }
}
