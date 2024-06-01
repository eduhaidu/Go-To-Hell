using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanGoreBlood : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CleanBlood());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CleanBlood(){
        yield return new WaitForSeconds(5.0f);
        Destroy(this.gameObject);
    }
}
