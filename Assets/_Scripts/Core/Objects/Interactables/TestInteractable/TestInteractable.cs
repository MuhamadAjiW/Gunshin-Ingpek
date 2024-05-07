using System;
using _Scripts.Core.Game.Data;
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
        if(!GameSaveData.Instance.storyData.IsEventComplete(StoryConfig.KEY_TEST_EVENT))
        {
            GameController.Instance.StartCutscene(StoryConfig.KEY_TEST_EVENT);
            GameSaveData.Instance.storyData.CompleteEvent(StoryConfig.KEY_TEST_EVENT);
        }
        else
        {
            Debug.Log("You already triggered the test cutscene");
        }
    }
}