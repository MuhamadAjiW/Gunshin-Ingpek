using System;
using UnityEngine;


[Serializable]
public class DummyAnimationController : AnimationController
{
    // Consts
    private const string HIT_TRIGGER = "Hit_param";

    // Attributes
    private Dummy dummy;

    // Constructor
    public void Init(Dummy dummy)
    {
        base.Init(dummy);
        this.dummy = dummy;
        dummy.OnDamagedEvent += IndicateDamaged;
        dummy.OnDamageDelayOverEvent += IndicateUnamaged;
    }

    // Functions
    private void IndicateDamaged()
    {
        renderer.material.color = Color.red;
    }

    private void IndicateUnamaged()
    {
        renderer.material.color = Color.white;
    }
}