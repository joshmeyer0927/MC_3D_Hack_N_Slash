using UnityEngine;

public class ImpactProjectile : MonoBehaviour
{
    [SerializeField] PooledMonoBehaviour impactParticle;

    ITakeHit entity;

    void Awake()
    {
        entity = GetComponent<ITakeHit>();

        if(entity != null)
            entity.OnHit += HandleHit;
    }

    void Destroy()
    {
        entity.OnHit -= HandleHit;
    }

    void HandleHit()
    {
        impactParticle.Get<PooledMonoBehaviour>(transform.position + new Vector3(0, 2, 0), Quaternion.identity);
    }
}