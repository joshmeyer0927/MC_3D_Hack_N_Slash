using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttacker : AbilityBase, IAttack
{
    [SerializeField] Projectile projectilePrefab;
    [SerializeField] float launchYOffset = 1f;
    [SerializeField] float launchDelay = .45f;

    public int Damage { get { return 1; } }

    public void Attack()
    {
        StartCoroutine(LaunchAfterDelay());
    }

    IEnumerator LaunchAfterDelay()
    {
        yield return new WaitForSeconds(launchDelay);
        projectilePrefab.Get<Projectile>(transform.position + Vector3.up * launchYOffset, transform.rotation);
    }

    protected override void OnUse()
    {
        Attack();
    }
}
