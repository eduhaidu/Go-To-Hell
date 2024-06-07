using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class AddToList : MonoBehaviour
{
    public GameObject col;
    // Start is called before the first frame update
    void Start()
    {
        col=GameObject.Find("GameObject");
        col.GetComponent<Collection>().nrs.Add(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
