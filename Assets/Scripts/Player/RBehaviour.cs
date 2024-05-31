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
    public float RocketJumpForce = 20.0f; // Adjust this value as needed

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasExploded)
        {
            print("Collided with " + collision.transform.gameObject.name);
            // Create Damage Radius
            RadiusDamageScript.SetActive(true);

            var boom = Instantiate(RocketExplosionPrefab, transform.position, Quaternion.identity); // DO NOT TOUCH THIS 

            GameObject player = GameObject.Find("Q3Player");
            if (Vector3.Distance(player.transform.position, this.transform.position) <= RocketJumpRadius)
            {
                var explosiondist = Vector3.Distance(this.transform.position, player.transform.position); // Store distance between rocket and player
                var playerController = player.GetComponent<Q3PlayerController>();
                playerController.distfromrocket = explosiondist;
                playerController.RocketJump(transform.position, RocketJumpForce);
            }

            print("Playing boom");

            RocketExplosionSource.PlayOneShot(RocketExplosionSound);
            gameObject.GetComponent<Renderer>().enabled = false;
            StartCoroutine(doBoom());
            hasExploded = true;
            StartCoroutine(cleanExplosion());
        }
    }

    IEnumerator cleanExplosion()
    {
        yield return new WaitForSeconds(3.0f);
        GameObject exp = GameObject.Find("P_RocketExplosion(Clone)");
        if (exp != null)
        {
            Destroy(exp);
        }
    }

    IEnumerator doBoom()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
    }
}
