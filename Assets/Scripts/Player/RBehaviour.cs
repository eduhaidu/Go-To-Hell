using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBehaviour : MonoBehaviour
{
    public ParticleSystem RocketExplosionPrefab;

    public AudioSource RocketExplosionSource;
    public AudioClip RocketExplosionSound;

    public bool hasExploded = false;

    public GameObject RadiusDamageScript;

    private void OnCollisionEnter(Collision collision)
    {
        if(!hasExploded){
            print("Collided with " + collision.transform.gameObject.name);
            //Create Damage Radius
            RadiusDamageScript.SetActive(true);

            var boom = Instantiate(RocketExplosionPrefab, transform.position, Quaternion.identity); //DO NOT FUCKING TOUCH THIS 
            print("Playing boom");

            RocketExplosionSource.PlayOneShot(RocketExplosionSound);  
            gameObject.GetComponent<Renderer>().enabled = false;
            StartCoroutine(doBoom());
            hasExploded = true;
            StartCoroutine(cleanExplosion());
        // Destroy(gameObject);
        // Destroy(gameObject);
        }
        
    }

    IEnumerator cleanExplosion(){
        while(true){
            yield return new WaitForSeconds(3.0f);
            GameObject exp = GameObject.Find("P_RocketExplosion(Clone)");
            Destroy(exp);
        }
        
    }

    IEnumerator doBoom()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
        yield return null;
    }
}
