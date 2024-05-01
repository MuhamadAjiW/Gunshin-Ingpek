
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
        Debug.Log("Test Interactable is interactable now");
        renderer.material.color = Color.yellow;
    }

    private void IndicateUninteractable()
    {
        Debug.Log("Test Interactable is uninteractable now");
        renderer.material.color = Color.white;
    }
}