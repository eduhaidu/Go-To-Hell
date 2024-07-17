using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class LeverScript : MonoBehaviour, IInteractable
{
    public bool isActivated=false;
    public void Interact()
    {
        isActivated=true;
        Debug.Log("Lever activated");
    }
}
