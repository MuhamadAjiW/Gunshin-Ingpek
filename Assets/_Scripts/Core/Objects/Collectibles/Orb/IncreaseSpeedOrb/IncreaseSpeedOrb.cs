using UnityEngine;

public class IncreaseSpeedOrb : Orb
{
    // Static Attributes
    public const string OBJECT_ID_PREFIX = "IncreaseSpeedOrb";

    // Attributes
    [SerializeField] float duration = 15f;

    private readonly StatEffect effect = new(
        "Increase Speed Orb",
        StatEffectType.SPEED,
        StatEffectType.MULTIPLICATION,
        0.2f,
        StatEffectFlag.INC_SPEED_ORB
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
        GameController.Instance.player.ActivateIncSpeedOrb(duration, effect);
    }
}