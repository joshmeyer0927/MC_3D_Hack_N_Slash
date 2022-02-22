using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : AbilityBase, IAttack
{
    [SerializeField] int damage = 1;
    [SerializeField] float attackOffset = 1f;
    [SerializeField] float attackRadius = 1f;
    [SerializeField] float attackImpactDelay = .5f;
    [SerializeField] float attackRange = 2f;
    
    LayerMask layerMask;
    Collider[] attackResults;

    public int Damage { get { return damage; } }

    void Awake()
    {
        string currentLayer = LayerMask.LayerToName(gameObject.layer);
        layerMask = ~LayerMask.GetMask(currentLayer);

        var animationImpactWatcher = GetComponentInChildren<AnimationImpactWatcher>();

        if(animationImpactWatcher != null)
            animationImpactWatcher.OnImpact += AnimationImpactWatcher_OnImpact;

        attackResults = new Collider[10];
    }

    /// <summary>
    /// Called by animation event via AnimationImpactWatcher
    /// </summary>
    void AnimationImpactWatcher_OnImpact()
    {
        /*Vector3 pos = transform.position + transform.forward * attackOffset;
        int hitCount = Physics.OverlapSphereNonAlloc(pos, attackRadius, attackResults, layerMask);

        for (int i = 0; i < hitCount; i++)
        {
            var takeHit = attackResults[i].GetComponent<ITakeHit>();

            if (takeHit != null)
                takeHit.TakeHit(this);
        }*/
    }

    public void Attack(ITakeHit target)
    {
        attackTimer = 0;

        StartCoroutine(DoAttack(target));
    }

    internal bool InAttackRange(ITakeHit target)
    {
        if (target.Alive == false)
            return false;

        var distance = Vector3.Distance(transform.position, target.transform.position);
        return distance < attackRange;
    }

    IEnumerator DoAttack(ITakeHit target)
    {
        yield return new WaitForSeconds(attackImpactDelay);

        if(target.Alive && InAttackRange(target))
            target.TakeHit(this);
    }

    public void Attack()
    {
        
    }

    protected override void OnUse()
    {
        //Attack();
    }
}