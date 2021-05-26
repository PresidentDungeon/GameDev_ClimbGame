using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMRotate : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] float speed;

    
    void Update()
    {
        transform.LookAt(target.transform);                             //Focuses on Island Object in Main Menu
        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }
}
