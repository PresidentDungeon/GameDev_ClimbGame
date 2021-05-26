using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderSpawner : MonoBehaviour
{
    public GameObject boulderPrefab;
    private ObjectPooling objectPool;

    private Vector3[] bouldersLeft = { new Vector3(121.5f, 36, -108.5f), new Vector3(121.5f, 28.5f, -108.5f), new Vector3(116, 36, -98.5f), new Vector3(116, 28.5f, -98.5f) };
    private Vector3[] bouldersRight = { new Vector3(116, 36, -61.5f), new Vector3(116, 28.5f, -61.5f), new Vector3(121.5f, 36, -51.5f), new Vector3(121.5f, 28.5f, -51.5f) };

    private float minRotationLeft = 45;
    private float maxRotationLeft = 80;

    private float minRotationRight = 100;
    private float maxRotationRight = 135;

    private float minForce = 500;
    private float maxForce = 800;

    private float intervalLow = 2;
    private float intervalHigh = 5;

    void Start()
    {
        this.objectPool = new ObjectPooling(boulderPrefab);
        Invoke("SpawnBoulder", intervalLow);
    }

    void SpawnBoulder()
    {
        int randomPositionLeft = Random.Range(0, bouldersLeft.Length - 1);
        int randomPositionRight = Random.Range(0, bouldersRight.Length - 1);
        float randomRotationLeft = Random.Range(minRotationLeft, maxRotationLeft);
        float randomRotationRight = Random.Range(minRotationRight, maxRotationRight);
        float randomForce = Random.Range(minForce, maxForce);

        GameObject boulderLeft = objectPool.getObject();
        GameObject boulderRight = objectPool.getObject();

        boulderLeft.transform.position = bouldersLeft[randomPositionLeft];
        boulderRight.transform.position = bouldersRight[randomPositionRight];

        boulderLeft.GetComponent<Rigidbody>().rotation = Quaternion.Euler(0, randomRotationLeft, 0);
        boulderRight.GetComponent<Rigidbody>().rotation = Quaternion.Euler(0, randomRotationRight, 0);

        boulderLeft.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * randomForce, ForceMode.Impulse);
        boulderRight.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * randomForce, ForceMode.Impulse);
        Invoke("SpawnBoulder", Random.Range(intervalLow, intervalHigh));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boulder"))
        {
            objectPool.releaseObject(other.gameObject);
        }
    }

}
