using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInitializer : MonoBehaviour
{
    public Vector3 startingLocation;
    public Vector3 startingRotation;
    public AudioClip levelSong;

    private GameManager gameManager;

    void Start()
    {
        this.gameManager = GameManager.GetInstance;         //Makes so you can use methods from GameManager
        Quaternion rotationQuaternion = Quaternion.Euler(startingRotation.x, startingRotation.y, startingRotation.z); //?

        gameManager.startingLocation = startingLocation;        //Sets Spawn location
        gameManager.startingRotation = rotationQuaternion;

        this.gameManager.playSong(levelSong);       //Sends the audio assigned in LevelData to the GameManager
    }
}
