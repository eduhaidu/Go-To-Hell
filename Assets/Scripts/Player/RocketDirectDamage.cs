using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketDirectDamage : MonoBehaviour
{
    public float DirectDamage = 65f;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyAI>().health-=DirectDamage;
        }
    }
}
