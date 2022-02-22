using UnityEngine;

public abstract class AbilityBase : MonoBehaviour
{
    [SerializeField] float attackRefreshSpeed = 1.5f;
    [SerializeField] PlayerButton button;
    [SerializeField] protected string animationTrigger;

    Controller controller;
    Animator animator;

    protected float attackTimer;

    protected abstract void OnUse();
    public bool CanAttack { get { return attackTimer >= attackRefreshSpeed; } }

    void Update()
    {
        if (animator == null)
            animator = GetComponentInChildren<Animator>();

        attackTimer += Time.deltaTime;

        if (ShouldTryUse())
        {
            if (string.IsNullOrEmpty(animationTrigger) == false)
                animator.SetTrigger(animationTrigger);

            OnUse();
        }
    }

    private bool ShouldTryUse()
    {
        return controller != null && CanAttack && controller.ButtonDown(button);
    }

    public void SetController(Controller controller)
    {
        this.controller = controller;
    }
}