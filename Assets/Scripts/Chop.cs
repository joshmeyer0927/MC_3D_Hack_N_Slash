using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chop : AbilityBase, IDamage
{
    [SerializeField] float attackRadius = 2f;
    [SerializeField] float impactDelay = .25f;
    [SerializeField] float forceAmount = 10f;
    [SerializeField] int damage = 1;

    Collider[] attackResults;
    LayerMask layerMask;

    public int Damage { get { return damage; } }

    void Awake()
    {
        attackResults = new Collider[10];

        string currentLayer = LayerMask.LayerToName(gameObject.layer);
        layerMask = ~LayerMask.GetMask(currentLayer);
    }

    void Attack()
    {
        StartCoroutine(DoAttack());
    }

    protected override void OnUse()
    {
        Attack();
    }

    IEnumerator DoAttack()
    {
        yield return new WaitForSeconds(impactDelay);

        Vector3 pos = transform.position + transform.forward;
        int hitCount = Physics.OverlapSphereNonAlloc(pos, attackRadius, attackResults, layerMask);

        for (int i = 0; i < hitCount; i++)
        {
            var takeHit = attackResults[i].GetComponent<ITakeHit>();

            if (takeHit != null)
                takeHit.TakeHit(this);

            var hitRigidbody = attackResults[i].GetComponent<Rigidbody>();

            if (hitRigidbody != null)
            {
                var direction = hitRigidbody.transform.position - transform.position;
                direction.Normalize();

                hitRigidbody.AddForce(direction * forceAmount, ForceMode.Impulse);
            }
        }
    }
}
