using System;
using UnityEngine;

public class TestInteractable : InteractableObject {
    // Attributes
    private TestInteractableAnimationController animationController;
    
    // Constructor
    protected void Start(){
        animationController = new TestInteractableAnimationController(this);
    }

    // Function
    public override void Interact(){
        Debug.Log("Test Interactable interacted");
    }
}