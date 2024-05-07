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
        if(!GameSaveData.Instance.storyData.IsEventComplete(StoryConfig.KEY_STORY1))
        {
            GameController.Instance.StartCutscene(StoryConfig.KEY_STORY1);
            GameSaveData.Instance.storyData.CompleteEvent(StoryConfig.KEY_STORY1);
        }
        else if(!GameSaveData.Instance.storyData.IsEventComplete(StoryConfig.KEY_STORY2))
        {
            GameController.Instance.StartCutscene(StoryConfig.KEY_STORY2);
            GameSaveData.Instance.storyData.CompleteEvent(StoryConfig.KEY_STORY2);
        }
        else if(!GameSaveData.Instance.storyData.IsEventComplete(StoryConfig.KEY_STORY3))
        {
            GameController.Instance.StartCutscene(StoryConfig.KEY_STORY3);
            GameSaveData.Instance.storyData.CompleteEvent(StoryConfig.KEY_STORY3);
        }
        else if(!GameSaveData.Instance.storyData.IsEventComplete(StoryConfig.KEY_STORY4))
        {
            GameController.Instance.StartCutscene(StoryConfig.KEY_STORY4);
            GameSaveData.Instance.storyData.CompleteEvent(StoryConfig.KEY_STORY4);
        }
        else if(!GameSaveData.Instance.storyData.IsEventComplete(StoryConfig.KEY_STORY5))
        {
            GameController.Instance.StartCutscene(StoryConfig.KEY_STORY5);
            GameSaveData.Instance.storyData.CompleteEvent(StoryConfig.KEY_STORY5);
        }
        else if(!GameSaveData.Instance.storyData.IsEventComplete(StoryConfig.KEY_STORY6))
        {
            GameController.Instance.StartCutscene(StoryConfig.KEY_STORY6);
            GameSaveData.Instance.storyData.CompleteEvent(StoryConfig.KEY_STORY6);
        }
        else if(!GameSaveData.Instance.storyData.IsEventComplete(StoryConfig.KEY_DEATH))
        {
            GameController.Instance.StartCutscene(StoryConfig.KEY_DEATH);
            GameSaveData.Instance.storyData.CompleteEvent(StoryConfig.KEY_DEATH);
        }
        else
        {
            GameController.Instance.StartCutscene(StoryConfig.KEY_TEST_EVENT);
            GameSaveData.Instance.storyData.CompleteEvent(StoryConfig.KEY_TEST_EVENT);
        }
    }
}