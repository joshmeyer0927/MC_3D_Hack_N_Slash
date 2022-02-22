using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathParticle : MonoBehaviour
{
    [SerializeField] PooledMonoBehaviour deathParticlePrefab;

    IDie entity;

    void Awake()
    {
        entity = GetComponent<IDie>();
    }

    void OnEnable()
    {
        entity.OnDied += Character_OnDied;
    }

    void Character_OnDied(IDie entity)
    {
        entity.OnDied -= Character_OnDied;

        deathParticlePrefab.Get<PooledMonoBehaviour>(transform.position, Quaternion.identity);
    }

    void OnDisable()
    {
        entity.OnDied -= Character_OnDied;
    }
}
