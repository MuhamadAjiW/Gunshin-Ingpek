using UnityEngine;

public class IncreaseDamageOrb : Orb
{
    // Static Attributes
    public const string OBJECT_ID_PREFIX = "IncreaseDamageOrb";
    private readonly StatEffect effect = new(
        "Increase Damage Orb",
        StatEffectType.DAMAGE,
        StatEffectType.MULTIPLICATION,
        0.1f
    );

    // Constructor
    protected new void Start()
    {
        base.Start();
        SetIdPrefix(OBJECT_ID_PREFIX);
    }

    // Functions
    protected override void OnCollect()
    {
        base.OnCollect();
        GameController.Instance.player.ActivateIncDamageOrb(effect);
    }

    protected override void OnTriggerEnter(Collider otherCollider)
    {
        if
        (
            GameController.Instance.player.incDamageOrbCount < GameController.Instance.player.maxIncDamageOrbCount
        )
        {
            base.OnTriggerEnter(otherCollider);
        }
    }
}