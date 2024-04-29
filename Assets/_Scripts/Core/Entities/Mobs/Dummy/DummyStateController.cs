public class DummyStateController : EntityStateController
{
    // Constructor
    public DummyStateController(Dummy dummy)
    {
    }
    
    // Functions
    protected override int DetectState()
    {
        return DefaultEntityState.IDLE;
    }
}