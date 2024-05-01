using System;
using UnityEngine;

public class TestInteractable : InteractableObject 
{
    // Attributes
    public TestInteractableAnimationController animationController;
    
    // Constructor
    protected void Start()
    {
        animationController.Init(this);
    }

    // Function
    public override void Interact()
    {
        Debug.Log("Test Interactable interacted");
    }
}