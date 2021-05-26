using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    private float leftLimit = -60;
    private float rightLimit = 100;

    // Update is called once per frame
    void Update()
    {
        //Destroy Boat Object when it reaches one of the sides
        if (transform.position.z < leftLimit || transform.position.z > rightLimit)
        {
            Destroy(gameObject);
        }
    }
}
