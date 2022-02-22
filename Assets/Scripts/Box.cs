using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour, ITakeHit
{
    [SerializeField] float forceAmount = 10f;

    Rigidbody rb;

    public bool Alive { get { return true; } }

    public event Action OnHit = delegate { };

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void TakeHit(IDamage hitBy)
    {
        var direction = transform.position - hitBy.transform.position;
        direction.Normalize();

        rb.AddForce(direction * forceAmount, ForceMode.Impulse);

        OnHit();
    }
}
