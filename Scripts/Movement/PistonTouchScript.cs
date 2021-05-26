using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonTouchScript : MonoBehaviour
{
    public float lerpDuration;
    public Vector3 endLocation;
    public Vector3 startLocation;

    void Start()
    {
        this.startLocation = transform.position;
    }

    private void FixedUpdate()
    {
        bool player = transform.childCount > 1;

        //Moves to one end, if Player is in collider
        if (player)
        {
            transform.position = Vector3.MoveTowards(transform.position, endLocation, Time.deltaTime * lerpDuration);
        }
        //Moves back to start, when Player is out of the collider
        else if (!player)
        {
            transform.position = Vector3.MoveTowards(transform.position, startLocation, Time.deltaTime * lerpDuration);
        }
    }
}
