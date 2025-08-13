using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardingPad : MonoBehaviour
{
    public bool isBoard;
    public Transform boat;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            isBoard = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            isBoard = false;
        }
    }
}
