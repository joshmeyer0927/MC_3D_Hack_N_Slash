using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : PooledMonoBehaviour, ITakeHit, IDie
{
    public static List<Character> All = new List<Character>();

    [SerializeField] int damage = 1;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] int maxHealth = 10;

    Controller controller;
    Animator animator;
    Vector3 direction;

    

    IAttack attacker;
    Rigidbody rb;

    int currentHealth;

    public event Action<int, int> OnHealthChanged = delegate { };
    public event Action<IDie> OnDied = delegate { };
    public event Action OnHit = delegate { };

    public int Damage { get { return damage; } }

    public bool Alive { get; private set; }

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        attacker = GetComponent<IAttack>();
    }

    internal void SetController(Controller controller)
    {
        this.controller = controller;

        foreach (var ability in GetComponents<AbilityBase>())
        {
            ability.SetController(controller);
        }
    }

    void Update()
    {
        if(controller != null)
            direction = controller.GetDirection();

        if (direction.magnitude > .25f)
        {
            var velocity = (direction * moveSpeed).With(y: rb.velocity.y);
            rb.velocity = velocity;

            transform.forward = direction * 360f;

            if(animator != null)
                animator.SetFloat("Speed", direction.magnitude);
        }
        else
        {
            if (animator != null)
                animator.SetFloat("Speed", 0);
        }
    }

    void OnEnable()
    {
        currentHealth = maxHealth;
        Alive = true;

        if (All.Contains(this) == false)
            All.Add(this);
    }

    protected override void OnDisable()
    {
        if (All.Contains(this))
            All.Remove(this);

        base.OnDisable();
    }

    public void TakeHit(IDamage hitBy)
    {
        currentHealth -= hitBy.Damage;
        OnHealthChanged(currentHealth, maxHealth);
        OnHit();

        if (currentHealth <= 0)
            Die();
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);

        OnHealthChanged(currentHealth, maxHealth);
    }

    void Die()
    {
        Alive = false;
        OnDied(this);
    }
}