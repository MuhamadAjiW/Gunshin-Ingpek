using UnityEngine;

public class Dummy : EnemyEntity
{
    // Static Attributes
    public const string OBJECT_ID_PREFIX = "Dummy";

    // Attributes
    private DummyAnimationController animationController;
    public DummyStateController stateController;

    // Constructor
    new protected void Start()
    {
        base.Start();
        SetIdPrefix(OBJECT_ID_PREFIX);
        stateController = new DummyStateController(this);
        animationController = new DummyAnimationController(this);
    }
}