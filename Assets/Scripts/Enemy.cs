using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Attacker))]
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : PooledMonoBehaviour, ITakeHit, IDie
{
    [SerializeField] int maxHealth = 3;

    Animator animator;
    Character target;
    NavMeshAgent navMeshAgent;
    Attacker attacker;

    int currentHealth;

    public event Action<int, int> OnHealthChanged = delegate { };
    public event Action<IDie> OnDied = delegate { };
    public event Action OnHit = delegate { };

    public bool IsDead { get { return currentHealth <= 0; } }

    public bool Alive { get; private set; }

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        attacker = GetComponent<Attacker>();
    }

    void OnEnable()
    {
        currentHealth = maxHealth;
        Alive = true;
    }

    void Update()
    {
        if (IsDead)
            return;

        if (target == null || target.Alive == false)
        {
            AquireTarget();
        }
        else
        {
            if (attacker.InAttackRange(target) == false)
            {
                FollowTarget();
            }
            else
            {
                TryAttack();
            }
        }
    }

    void AquireTarget()
    {
        target = Character.All
                        .OrderBy(t => Vector3.Distance(transform.position, t.transform.position))
                        .FirstOrDefault();

        animator.SetFloat("Speed", 0);
    }

    void FollowTarget()
    {
        animator.SetFloat("Speed", 1);
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(target.transform.position);
    }

    void TryAttack()
    {
        animator.SetFloat("Speed", 0);
        navMeshAgent.isStopped = true;

        if (attacker.CanAttack)
        {
            animator.SetTrigger("Attack");
            attacker.Attack(target);
        }
    }

    public void TakeHit(IDamage hitBy)
    {
        currentHealth -= hitBy.Damage;

        OnHealthChanged(currentHealth, maxHealth);

        OnHit();

        if (currentHealth <= 0)
            Die();
        else
            animator.SetTrigger("Hit");
    }

    void Die()
    {
        animator.SetTrigger("Die");
        navMeshAgent.isStopped = true;

        Alive = false;
        OnDied(this);

        ReturnToPool(6f);
    }
}