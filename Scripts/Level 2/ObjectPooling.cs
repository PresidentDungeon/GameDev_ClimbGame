using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling {

    private GameObject prefabObject;
    private List<GameObject> objectsInUse = new List<GameObject>();
    private List<GameObject> releasedObjects = new List<GameObject>();

    public ObjectPooling(GameObject prefabObject)
    {
        this.prefabObject = prefabObject;
    }

    public GameObject getObject()
    {
        if(releasedObjects.Count <= 0)
        {
            GameObject gameObject = GameObject.Instantiate(prefabObject);
            objectsInUse.Add(gameObject);
            return gameObject;
        }
        else
        {
            GameObject gameObject = releasedObjects[0];
            releasedObjects.Remove(gameObject);
            objectsInUse.Add(gameObject);
            gameObject.SetActive(true);
            return gameObject;
        }
    }

    public void releaseObject(GameObject objectToRelease)
    {
        objectToRelease.SetActive(false);
        releasedObjects.Add(objectToRelease);
        objectsInUse.Remove(objectToRelease);
    }

    public void clearObjects()
    {
        objectsInUse.ForEach((gameObject) => { GameObject.Destroy(gameObject); });
        releasedObjects.ForEach((gameObject) => { GameObject.Destroy(gameObject); });
        objectsInUse = new List<GameObject>();
        releasedObjects = new List<GameObject>();
    }

}
