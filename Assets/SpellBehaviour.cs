using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBehaviour : MonoBehaviour
{
    public int SpellDamage = 50;
    public bool hasHit = false;

    private void OnCollisionEnter(Collision collision){
        if(!hasHit){
            print("Collided with" + collision.transform.gameObject.name);
            GameObject player = GameObject.Find("Q3Player");
            if(collision.transform.gameObject.name=="Q3Player"){
                player.GetComponent<player_health>().playerHealth-=SpellDamage;
                if(player.GetComponent<player_health>().playerHealth<=0){
                    player.GetComponent<player_health>().PlayerDeath();
                }
            }
            hasHit=true;
            Destroy(this.gameObject);
        }
    }
}
