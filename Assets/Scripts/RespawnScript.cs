using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    public Transform spawnPoint;

    public void OnTriggerEnter(Collider other)
    {
        other.gameObject.transform.position = spawnPoint.position; 
    }
}
