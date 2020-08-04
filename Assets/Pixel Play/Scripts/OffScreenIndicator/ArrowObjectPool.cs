using System.Collections.Generic;
using UnityEngine;

class ArrowObjectPool : MonoBehaviour
{
    public static ArrowObjectPool current;

    [Tooltip("Assign the arrow prefab.")]
    public Indicator pooledObject = null;
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
                Indicator arrow = Instantiate(pooledObject);
                arrow.transform.SetParent(transform, false);
                arrow.Activate(false);
                pooledObjects.Add(arrow);
            }
        }
        catch
        {
            Debug.Log("ArrowObjectPool.Start Error");
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
                Indicator arrow = Instantiate(pooledObject);
                arrow.transform.SetParent(transform, false);
                arrow.Activate(false);
                pooledObjects.Add(arrow);
                return arrow;
            }
            return null;
        }
        catch
        {
            Debug.Log("ArrowObjectPool.GetPooledObject Error");
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
            foreach (Indicator arrow in pooledObjects)
            {
                arrow.Activate(false);
            }
        }
        catch
        {
            Debug.Log("ArrowObjectPool.DeactivateAllPooledObjects Error");
        }
    }
}
