public class DummyStateController : DamageableEntityStateController
{
    // Constructor
    public DummyStateController(Dummy dummy) : base(dummy)
    {
    }
    
    // Functions
    public override int UpdateState()
    {
        return DefaultEntityState.IDLE;
    }
}