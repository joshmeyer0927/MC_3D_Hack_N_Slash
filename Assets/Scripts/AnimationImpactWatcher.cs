using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationImpactWatcher : MonoBehaviour
{
    public event Action OnImpact;

    /// <summary>
    /// Called by Animation
    /// </summary>
    void Impact()
    {
        if(OnImpact != null)
            OnImpact();
    }
}
