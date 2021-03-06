﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : ScriptableObject
{
    public List<GameObject> pooledObjects;
    public GameObject Parent;
    GameObject objectToPool;
    int amountToPool;
    bool shouldExpand;

    public void SetPool(GameObject baseObj, int amount, bool expand, GameObject parent = null)
    {
        objectToPool = baseObj;
        amountToPool = amount;
        shouldExpand = expand;

        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(objectToPool);
            if (parent != null)
                obj.transform.SetParent(parent.transform, false);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
                return pooledObjects[i];
        }
        if (shouldExpand)
        {
            GameObject obj = (GameObject)Instantiate(objectToPool);
            obj.SetActive(false);
            pooledObjects.Add(obj);
            return obj;
        }
        else
            return null;
    }

    public void EmptyPool()
    {
        foreach (var obj in pooledObjects)
            obj.SetActive(false);
    }
}
