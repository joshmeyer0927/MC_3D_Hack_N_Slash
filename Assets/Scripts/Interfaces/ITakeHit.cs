using System;
using UnityEngine;

public interface ITakeHit
{
    Transform transform { get; }

    bool Alive { get; }

    event Action OnHit;

    void TakeHit(IDamage hitBy);
}