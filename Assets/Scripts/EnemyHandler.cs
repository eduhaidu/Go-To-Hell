using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public int Health = 100;
    public Animator anima;



    private void Update()
    {
       if(Health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        anima.SetBool("Die", true);

        //Destroy(this.gameObject);
        Debug.Log("Enemy killed");
    }
}
