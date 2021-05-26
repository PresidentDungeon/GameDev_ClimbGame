using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject boatPrefab;
    private ObjectPooling objectPool;

    private float[] spawnLeft = { 55, 75, 115, 155, 175 };
    private float[] spawnRight = { 65, 105, 125, 165, 185 };

    private float spawnY = 3.5f;
    private float spawnZLeft = 100;
    private float spawnZRight = -60;

    private Quaternion rotateLeft = Quaternion.Euler(0, 180, 0);
    private Quaternion rotateRight = Quaternion.Euler(0, 0, 0);

    private float spawnDelay = 10;
    private float repeatRate1 = 1;
    private float repeatRate2 = 5;

    private bool shouldSpawn = true;

    void Start()
    {
        this.objectPool = new ObjectPooling(boatPrefab);
        Invoke("SpawnBoats", 0);
    }

    IEnumerator SpawnBoat(Vector3 spawnLocation, Quaternion spawnRotation, float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject boat = objectPool.getObject();
        boat.transform.position = spawnLocation;
        boat.transform.rotation = spawnRotation;
    }

    void SpawnBoats()
    {
        if (shouldSpawn)
        {
            foreach (float xLocation in spawnLeft)
            {
                float spawnInterval = Random.Range(repeatRate1, repeatRate2);

                StartCoroutine(SpawnBoat(new Vector3(xLocation, spawnY, spawnZLeft), rotateLeft, spawnInterval));
            }

            foreach (float xLocation in spawnRight)
            {
                float spawnInterval = Random.Range(repeatRate1, repeatRate2);

                StartCoroutine(SpawnBoat(new Vector3(xLocation, spawnY, spawnZRight), rotateRight, spawnInterval));
            }

            Invoke("SpawnBoats", spawnDelay);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.parent && other.gameObject.transform.parent.CompareTag("Boat"))
        {
            objectPool.releaseObject(other.gameObject.transform.parent.gameObject);
        }
    }

    public void stopSpawn()
    {
        this.shouldSpawn = false;
        StopAllCoroutines();
        StartCoroutine(destroyObjects());
    }

    private IEnumerator destroyObjects()
    {
        const float awaitTime = 26f;
        yield return new WaitForSeconds(awaitTime);
        objectPool.clearObjects();
        Destroy(gameObject);
    }

}
