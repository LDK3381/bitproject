﻿using System.Collections.Generic;
using UnityEngine;

public class BoxObjectPool : MonoBehaviour
{
    public static BoxObjectPool current;

    [Tooltip("Assign the box prefab.")]
    public Indicator pooledObject;
    [Tooltip("Initial pooled amount.")]
    public int pooledAmount = 1;
    [Tooltip("Should the pooled amount increase.")]
    public bool willGrow = true;

    List<Indicator> pooledObjects;

    void Awake()
    {
        current = this;
    }

    void Start()
    {
        try
        {
            pooledObjects = new List<Indicator>();

            for (int i = 0; i < pooledAmount; i++)
            {
                Indicator box = Instantiate(pooledObject);
                box.transform.SetParent(transform, false);
                box.Activate(false);
                pooledObjects.Add(box);
            }
        }
        catch
        {
            Debug.Log("BoxObjectPool.Start Error");
        }
    }

    /// <summary>
    /// Gets pooled objects from the pool.
    /// </summary>
    /// <returns></returns>
    public Indicator GetPooledObject()
    {
        try
        {
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                if (!pooledObjects[i].Active)
                {
                    return pooledObjects[i];
                }
            }
            if (willGrow)
            {
                Indicator box = Instantiate(pooledObject);
                box.transform.SetParent(transform, false);
                box.Activate(false);
                pooledObjects.Add(box);
                return box;
            }
            return null;
        }
        catch
        {
            Debug.Log("BoxObjectPool.GetPooledObject Error");
            return null;
        }
    }

    /// <summary>
    /// Deactive all the objects in the pool.
    /// </summary>
    public void DeactivateAllPooledObjects()
    {
        try
        {
            foreach (Indicator box in pooledObjects)
            {
                box.Activate(false);
            }
        }
        catch
        {
            Debug.Log("BoxObjectPool.DeactivateAllPooledObjects Error");
        }
    }
}
