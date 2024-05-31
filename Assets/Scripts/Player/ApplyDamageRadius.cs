using System.Collections;
using System.Collections.Generic;
using Q3Movement;
using Unity.VisualScripting;
using UnityEngine;

public class ApplyDamageRadius : MonoBehaviour
{
    public GameObject RocketExplosionRadius;
    public GameObject Player;
    public int radiusDamage = 65;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            var enemy = other.gameObject.GetComponent<EnemyAI>();
             float distanceFromRocket = Vector3.Distance(this.transform.position,enemy.transform.position);
             float maxDamageDistance = 8.0f;
             float falloffFactor = Mathf.Clamp01(1-(distanceFromRocket/maxDamageDistance));
             int damage = Mathf.RoundToInt(radiusDamage*falloffFactor);
            enemy.health -= damage;
            Debug.Log("Enemy takes damage");
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }


}