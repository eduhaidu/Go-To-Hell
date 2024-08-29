using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public DoorClassCreate[] doors;


    private void Update()
    {
        for(int i = 0; i < doors.Length; i++)
        {
            doors[i].doorPiece.transform.position = Vector3.MoveTowards(doors[i].doorPiece.transform.position, doors[i].pieceTarget.transform.position,1.0f*Time.deltaTime) ;
        }
    }
}

