
using UnityEngine;

public class TestInteractableAnimationController : AnimationController
{
    // Attributes
    private readonly TestInteractable testInteractable;

    // Constructor
    public TestInteractableAnimationController(TestInteractable testInteractable) : base(testInteractable)
    {
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