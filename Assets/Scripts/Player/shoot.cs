using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    [SerializeField]
    private GameObject currentHit; //get enemy object to reuse in other functions do not touch this
    //VISUAL EFFECT VARIABLES ONLY
    public Light weaponflash;
    public float weaponvisualsfrequency = 0.2f; //How fast should the muzzle flash?

    public Vector3 BarrelRotationVector; // Your custom Vector3 representing desired rotation
    public float BarrelRotationSpeed = 1.0f;
    public GameObject GunBarrel;

    public int damage=13;
    public float range=100f;
    public int bulletCount=300;
    public int rocketCount = 3;

    public Transform origin;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetMouseButton(0)){
            if(bulletCount>0){
                StartCoroutine(WeaponVisuals());
                StartCoroutine(FireGun());
                RotateBarrel();
            }
            else
            {
                Debug.Log("Out of bullets");
            }
        }
    }

    IEnumerator FireGun()
    {
        RaycastHit hit;
        yield return new WaitForSeconds(0.3f);

        if (Physics.Raycast(origin.transform.position, origin.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Debug.DrawLine(origin.transform.position, hit.point);
            if (hit.transform.gameObject.tag == "Enemy")
            {
                currentHit = hit.transform.gameObject;
                DoDamage();
            }
        }

        bulletCount--;
        yield return null;
    }

    IEnumerator WeaponVisuals()
    {
        weaponflash.enabled = !weaponflash.enabled;
        yield return new WaitForSeconds(weaponvisualsfrequency);
        weaponflash.enabled = false;
        yield return null;
    }

    void DoDamage()
    {
        var enemyhp = currentHit.gameObject.GetComponent<EnemyAI>();
        enemyhp.health-= damage;

        print("Did damage");
    }

    void RotateBarrel()
    {
        //var zrot = GunBarrel.transform.rotation.z;

        GunBarrel.transform.Rotate(Vector3.forward*BarrelRotationSpeed*Time.deltaTime);
        Debug.Log("rotating barrel");
    }

    }