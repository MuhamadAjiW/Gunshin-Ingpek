using UnityEngine;

public class Dummy : EnemyEntity
{
    // Static attributes
    public const string ObjectIdPrefix = "Dummy";

    // Attributes
    private DummyAnimationController animationController;
    public DummyStateController stateController;

    // Constructor
    new protected void Start()
    {
        base.Start();
        SetIdPrefix(ObjectIdPrefix);
        stateController = new DummyStateController(this);
        animationController = new DummyAnimationController(this);
    }
}