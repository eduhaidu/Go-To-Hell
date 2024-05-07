using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject BoiledOnePrefab;
    [SerializeField] private float spawnerInterval=3.5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(spawnerInterval,BoiledOnePrefab));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy){
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(0,0,0),Quaternion.identity);
        StartCoroutine(spawnEnemy(interval,enemy));
    }
}
