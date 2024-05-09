using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject PlayerPrefab;

    private void Awake()
    {
        Instantiate(PlayerPrefab,this.gameObject.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
