using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBoardScript : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private bool activated = false;

    private void OnTriggerEnter(Collider other)
    {
        //Plays the recorded animation when Player enters the box collider
        if (other.CompareTag("Player") && !activated)
        {
            activated = true;                       //Boolean so animation doesn't loop
            this.animator.Play("FallingBoard");     //Recorded animation called "FallingBoard"
        }
    }


}
