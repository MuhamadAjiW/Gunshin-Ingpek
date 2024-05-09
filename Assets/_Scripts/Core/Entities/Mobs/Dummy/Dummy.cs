using UnityEngine;

public class Dummy : EnemyEntity
{
    // Static Attributes
    public const string OBJECT_ID_PREFIX = "Dummy";

    // Attributes
    public DummyAnimationController animationController;
    public AudioController audioController;
    public DummyStateController stateController;

    // Constructor
    new protected void Start()
    {
        base.Start();
        SetIdPrefix(OBJECT_ID_PREFIX);
        stateController.Init(this);
        animationController.Init(this);
        audioController.Init(this);
        OnDeathEvent += OnDeath;
    }

    private void OnDeath()
    {
        GameController.Instance.InvokeEvent(GameConfig.EVENT_ENEMY_KILLED, this);
    }
}