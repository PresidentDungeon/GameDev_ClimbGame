using UnityEngine;

public class AttachScript : MonoBehaviour
{
    private bool isAttached = false;
    private bool firstAttach = true;

    //Syncs up Players movement to the object, when in collider
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !isAttached && !firstAttach)
        {
            other.transform.parent = transform.parent;
            isAttached = true;
        }
        else if (other.CompareTag("Player") && firstAttach)
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement.isGrounded)
            {
                firstAttach = false;
                isAttached = false;
            }
        }
    }

    //Unsyncs player, when exits collider 
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
            this.firstAttach = true;
            this.isAttached = false;
        }
    }
}
