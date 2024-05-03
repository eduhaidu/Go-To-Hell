using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public int Health = 100;




    private void Update()
    {
       if(Health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //Destroy object debug purpose

        Destroy(this.gameObject);
        Debug.Log("Enemy killed");
    }
}
