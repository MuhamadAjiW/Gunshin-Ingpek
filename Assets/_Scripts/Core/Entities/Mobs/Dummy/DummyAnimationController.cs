using UnityEngine;

public class DummyAnimationController : AnimationController
{
    // Consts
    private const string HIT_TRIGGER = "hit_param";

    // Attributes
    private readonly Dummy dummy;

    // Constructor
    public DummyAnimationController(Dummy dummy) : base(dummy) 
    {
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