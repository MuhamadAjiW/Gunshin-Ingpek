public class Dummy : EnemyEntity{
    // Attributes
    private DummyAnimationController animationController;
    public DummyStateController stateController;

    // Constructor
    new void Start(){
        base.Start();
        stateController = new DummyStateController(this);
        animationController = new DummyAnimationController(this);
    }
}