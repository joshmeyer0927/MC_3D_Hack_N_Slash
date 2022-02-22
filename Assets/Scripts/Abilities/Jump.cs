using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : AbilityBase
{
    [SerializeField] float jumpForce = 300f;

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected override void OnUse()
    {
        rb.AddForce(Vector3.up * jumpForce);
    }
}
