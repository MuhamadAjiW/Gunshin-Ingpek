using UnityEngine;

public class RestoreHealthOrb : Orb
{
    // Static Attributes
    public const string OBJECT_ID_PREFIX = "RestoreHealthOrb";

    // Attributes
    [SerializeField] float healthMultiplier = 0.2f;

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
        GameController.Instance.player.ActivateRestoreHealthOrb(healthMultiplier);
    }
}