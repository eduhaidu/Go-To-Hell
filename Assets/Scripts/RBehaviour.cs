using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBehaviour : MonoBehaviour
{
    public ParticleSystem RocketExplosionPrefab;

    public AudioSource RocketExplosionSource;
    public AudioClip RocketExplosionSound;

    private void OnCollisionEnter(Collision collision)
    {
        print("Collided with " + collision.transform.gameObject.name);
        var boom = Instantiate(RocketExplosionPrefab, transform.position, Quaternion.identity); //DO NOT FUCKING TOUCH THIS 
        print("Playing boom");
        RocketExplosionSource.PlayOneShot(RocketExplosionSound);
        StartCoroutine(doBoom());
        // Destroy(gameObject);
        // Destroy(gameObject);
    }

    IEnumerator doBoom()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
        yield return null;
    }
}
