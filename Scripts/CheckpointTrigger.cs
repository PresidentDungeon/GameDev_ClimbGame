using System.Collections;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    [SerializeField] private Vector3 checkpointLocation;
    [SerializeField] private Vector3 checkpointRotation;
    private GameManager gameManager;
    private AudioSource audioSource;

    private bool isActive = false;

    [SerializeField] private GameObject activeEffect;
    [SerializeField] private GameObject pickupEffect;
    

    private void Start()
    {
        this.gameManager = GameManager.GetInstance;
        this.audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!this.isActive && other.tag == "Player")
        {
            this.isActive = true;
            gameManager.startingLocation = checkpointLocation;
            gameManager.startingRotation = Quaternion.Euler(checkpointRotation.x, checkpointRotation.y, checkpointRotation.z);
            activeEffect.SetActive(false);
            this.audioSource.Play();
            Instantiate(pickupEffect, transform.position, transform.rotation);
            StartCoroutine(Destroy());
        }
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
