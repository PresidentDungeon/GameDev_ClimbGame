using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatStopTrigger : MonoBehaviour
{
    [SerializeField] private SpawnManager spawnManager;
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        //Stops spawning boats, when Player reaches a certain point
        if (other.CompareTag("Player") && !triggered)
        {
            triggered = true;
            spawnManager.stopSpawn();
        }
    }

}
