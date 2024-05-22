using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyDamageRadius : MonoBehaviour
{
    public int radiusDamage = 65;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            var enemy = other.gameObject.GetComponent<EnemyAI>();
            enemy.health -= radiusDamage;

            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }


}
