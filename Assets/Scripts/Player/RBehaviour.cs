using System.Collections;
using System.Collections.Generic;
using Q3Movement;
using UnityEngine;

public class RBehaviour : MonoBehaviour
{
    public ParticleSystem RocketExplosionPrefab;

    public AudioSource RocketExplosionSource;
    public AudioClip RocketExplosionSound;

    public bool hasExploded = false;

    public GameObject RadiusDamageScript;

    public float RocketJumpRadius = 3.0f;

    private void OnCollisionEnter(Collision collision)
    {
        if(!hasExploded){
            print("Collided with " + collision.transform.gameObject.name);
            //Create Damage Radius
            RadiusDamageScript.SetActive(true);

            var boom = Instantiate(RocketExplosionPrefab, transform.position, Quaternion.identity); //DO NOT FUCKING TOUCH THIS 
            //var explRadius = gameObject.GetComponent<SphereCollider>().radius;

            //WIP
            if(Vector3.Distance(GameObject.Find("Q3Player").transform.position,this.transform.position)<=RocketJumpRadius){
                var explosiondist = Vector3.Distance(this.transform.position, GameObject.Find("Q3Player").transform.position); //Store distance between rocket and player
                var playerref = GameObject.Find("Q3Player");
                playerref.GetComponent<Q3PlayerController>().distfromrocket = explosiondist;
                GameObject.Find("Q3Player").GetComponent<Q3PlayerController>().RocketJump();
                //Debug.Log("A sarit");
            } 

            //WIP
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
