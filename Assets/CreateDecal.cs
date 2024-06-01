using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CreateDecal : MonoBehaviour
{
    public GameObject m_DecalProjectorPrefab;
    // Start is called before the first frame update
    void Start()
    {
        GameObject m_DecalProjectorObject = Instantiate(m_DecalProjectorPrefab,this.gameObject.transform.position,m_DecalProjectorPrefab.transform.rotation);
        DecalProjector m_DecalProjectorComponent = m_DecalProjectorObject.GetComponent<DecalProjector>();

        // Creates a new material instance for the DecalProjector.
        m_DecalProjectorComponent.material = new Material(m_DecalProjectorComponent.material);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
