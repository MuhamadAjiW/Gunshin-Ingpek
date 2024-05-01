using System;
using UnityEngine;

[Serializable]
public class DummyStateController : EntityStateController
{
    // Constructor
    public void Init(Dummy dummy)
    {
    }
    
    // Functions
    protected override int DetectState()
    {
        return DefaultEntityState.IDLE;
    }
}