using UnityEngine;

public class Dummy : EnemyEntity{
    // Attributes
    private DummyAnimationController animationController;
    public DummyStateController stateController;

    // Constructor
    new protected void Start(){
        base.Start();
        stateController = new DummyStateController(this);
        animationController = new DummyAnimationController(this);
    }
}