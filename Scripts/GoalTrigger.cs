using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    private bool isActive = false;
    private GameManager manager;

    private void Start()
    {
        this.manager = GameManager.GetInstance; //Makes it possible to call methods in GameManager
    }

    private void OnTriggerEnter(Collider other)
    {
        //Ends the game, when Player enters collider
        if (!this.isActive && other.tag == "Player")
        {
            this.isActive = true;
            manager.finishGame();                       //Calls the method from GameManager
        }
    }
}
