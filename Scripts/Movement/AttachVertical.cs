using System.Collections;
using UnityEngine;

public class AttachVertical : MonoBehaviour
{
    [SerializeField] private PistonScript pistonScript;
    private PlayerMovement player;
    private bool isAttached = false;
    private bool firstAttach = true;
    private Coroutine activeCoroutine;

    private void FixedUpdate()
    {
        if (player != null && !player.isGrounded && isAttached && pistonScript.forwards)
        {
            player.transform.parent = null;
            isAttached = false;
            firstAttach = true;
        }
        else if (player != null && pistonScript.isWaiting && !player.isGrounded && isAttached)
        {
            player.transform.parent = null;
            isAttached = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !isAttached && !firstAttach)
        {

            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if ((!pistonScript.isWaiting && !pistonScript.forwards) || (pistonScript.forwards && playerMovement.isGrounded))
            {
                if (pistonScript.forwards){playerMovement.setPlayerVelocity(pistonScript.velocity);}

                player = playerMovement;
                other.transform.parent = transform.parent;
                isAttached = true;
            }
        }
        else if (other.CompareTag("Player") && firstAttach)
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement.isGrounded)
            {
                if (pistonScript.forwards){playerMovement.setPlayerVelocity(pistonScript.velocity);}

                firstAttach = false;
                isAttached = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && player != null)
        {
            if (activeCoroutine != null) { StopCoroutine(activeCoroutine); }
            this.activeCoroutine = StartCoroutine(exitParent());
        }
    }

    private IEnumerator exitParent()
    {

        while (true)
        {
            if (isPlayerAttached())
            {
                if (this.activeCoroutine != null)
                {
                    player.transform.parent = null;
                    player = null;
                    isAttached = false;
                    firstAttach = true;
                    StopCoroutine(activeCoroutine);
                    break;
                }
            }

            else if (player.isGrounded)
            {
                player.transform.parent = null;
                player = null;
                isAttached = false;
                firstAttach = true;
                StopCoroutine(activeCoroutine);
                break;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    private bool isPlayerAttached()
    {
        if (player != null)
        {
            return player.transform.parent != null;
        }
        return false;
    }
}
