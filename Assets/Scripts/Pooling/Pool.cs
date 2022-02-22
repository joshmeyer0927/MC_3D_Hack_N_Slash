using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    static Dictionary<PooledMonoBehaviour, Pool> pools = new Dictionary<PooledMonoBehaviour, Pool>();
    Queue<PooledMonoBehaviour> objects = new Queue<PooledMonoBehaviour>();

    PooledMonoBehaviour prefab;

    public static Pool GetPool(PooledMonoBehaviour prefab)
    {
        if (pools.ContainsKey(prefab))
            return pools[prefab];

        var poolGameObject = new GameObject("Pool-" + prefab.name);
        var pool = poolGameObject.AddComponent<Pool>();

        pool.prefab = prefab;

        pools.Add(prefab, pool);

        return pool;
    }

    public T Get<T>() where T : PooledMonoBehaviour
    {
        if (objects.Count == 0)
            GrowPool();

        var pooledObject = objects.Dequeue();

        return pooledObject as T;
    }

    void GrowPool()
    {
        for (int i = 0; i < prefab.InitialPoolSize; i++)
        {
            var pooledObject = Instantiate(prefab) as PooledMonoBehaviour;

            pooledObject.gameObject.name += " " + i;

            pooledObject.OnReturnToPool += AddObjectToAvailableQueue;

            pooledObject.transform.SetParent(this.transform);
            pooledObject.gameObject.SetActive(false);
        }
    }

    void AddObjectToAvailableQueue(PooledMonoBehaviour pooledObject)
    {
        objects.Enqueue(pooledObject);
    }
}