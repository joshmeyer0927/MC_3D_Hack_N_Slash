using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealOnHit : MonoBehaviour, ITakeHit
{
    [SerializeField] int healAmount = 10;
    [SerializeField] bool disableOnUse = true;

    public bool Alive { get { return true; } }

    public event Action OnHit = delegate { };

    public void TakeHit(IDamage hitBy)
    {
        var character = hitBy.transform.GetComponent<Character>();

        if (character != null)
        {
            OnHit();
            character.Heal(healAmount);

            if (disableOnUse)
                gameObject.SetActive(false);
        }
    }
}
