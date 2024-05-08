
using System;
using UnityEngine;

[Serializable]
public class TestInteractableAnimationController : AnimationController
{
    // Attributes
    private TestInteractable testInteractable;

    // Constructor
    public void Init(TestInteractable testInteractable)
    {
        base.Init(testInteractable);
        this.testInteractable = testInteractable;
        testInteractable.OnInteractAreaEnterEvent += IndicateInteractable;
        testInteractable.OnInteractAreaExitEvent += IndicateUninteractable;
    }

    // Functions
    private void IndicateInteractable()
    {
        renderer.material.color = Color.yellow;
    }

    private void IndicateUninteractable()
    {
        renderer.material.color = Color.white;
    }
}