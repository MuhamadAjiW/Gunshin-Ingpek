public class DummyStateController : EntityStateController
{
    // Constructor
    public DummyStateController(Dummy dummy)
    {
    }
    
    // Functions
    public override int UpdateState()
    {
        return DefaultEntityState.IDLE;
    }
}