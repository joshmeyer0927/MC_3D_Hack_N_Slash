using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : PooledMonoBehaviour, IDamage
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] PooledMonoBehaviour impactParticlePrefab;

    public int Damage { get { return 1; } }

    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * moveSpeed;
    }

    void OnCollisionEnter(Collision collision)
    {
        var hit = collision.collider.GetComponent<ITakeHit>();

        if (hit != null)
            Impact(hit);
        else
        {
            impactParticlePrefab.Get<PooledMonoBehaviour>(transform.position, Quaternion.identity);
            ReturnToPool();
        }
    }

    void Impact(ITakeHit hit)
    {
        impactParticlePrefab.Get<PooledMonoBehaviour>(transform.position, Quaternion.identity);

        hit.TakeHit(this);

        ReturnToPool();
    }
}
