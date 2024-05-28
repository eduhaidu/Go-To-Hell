using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RLauncher : MonoBehaviour
{
    public GameObject RocketPrefab;
    public float RocketSpeed = 1.0f;
    //public int RocketCount;
    public Transform RocketSpawn;
    public Transform RocketOrientation;

    public GameObject WeaponScriptHolder; //references the shoot script that holds count of rockets

    public AudioSource RocketLauncherSource;
    public AudioClip RocketLauncherSound;

    private bool canShoot = true;
    public float delayBetweenRockets = 2.7f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1)&&canShoot)
        {
            StartCoroutine(InstantiateRocketWithDelay());
           
            //Take one rocket away from rocket script
            var weaponscr = WeaponScriptHolder.GetComponent<shoot>();
            if (weaponscr.rocketCount > 0)
            {
                weaponscr.rocketCount--;

            }
        }
    }

    IEnumerator InstantiateRocketWithDelay(){
        canShoot = false;
        instantiateRocket();
        yield return new WaitForSeconds(delayBetweenRockets);
        canShoot = true;
    }

    public void instantiateRocket()
        {
            if (WeaponScriptHolder.GetComponent<shoot>().rocketCount > 0)
            {
            RocketLauncherSource.PlayOneShot(RocketLauncherSound);
                var rocket = Instantiate(RocketPrefab, RocketSpawn.position, Quaternion.identity);

                rocket.transform.forward = RocketSpawn.forward; // Set forward direction (optional)
                rocket.GetComponent<Rigidbody>().velocity = rocket.transform.forward * RocketSpeed;
            }
        }
    }
