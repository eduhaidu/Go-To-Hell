using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaBehaviour : MonoBehaviour
{
    public int MagmaDamage=50;
    public bool hasExploded=false;
    
    private void OnCollisionEnter(Collision collision){
        if(!hasExploded){
            print("Collided with " + collision.transform.gameObject.name);
            GameObject player = GameObject.Find("Q3Player");
            if (collision.transform.gameObject.name=="Q3Player"){
                player.GetComponent<player_health>().playerHealth-=MagmaDamage;
                if(player.GetComponent<player_health>().playerHealth<=0){
                    player.GetComponent<player_health>().PlayerDeath();
                }    
            }
            hasExploded=false;
            Destroy(this.gameObject);
        }
    }
}
