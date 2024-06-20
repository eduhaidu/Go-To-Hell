using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaLauncher : MonoBehaviour
{
    public GameObject MagmaProjectile;
    public float ProjectileSpeed=20.0f;
    public Transform MagmaSpawn;
    public bool canShoot=true;
    public float delayBetweenProjectiles=2.2f;

    // Update is called once per frame
    void Update()
    {
        if(canShoot){
            StartCoroutine(shootWithDelay());
        }
    }

    IEnumerator shootWithDelay(){
        canShoot=false;
        shootMagma();
        yield return new WaitForSeconds(delayBetweenProjectiles);
        canShoot=true;
    }

    public void shootMagma(){
        var projectile = Instantiate(MagmaProjectile,MagmaSpawn.position,Quaternion.identity);
        projectile.transform.forward=MagmaSpawn.forward;
        projectile.GetComponent<Rigidbody>().velocity=projectile.transform.forward*ProjectileSpeed;
    }
}
