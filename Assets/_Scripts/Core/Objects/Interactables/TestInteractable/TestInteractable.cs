using System;
using _Scripts.Core.Game.Data;
using UnityEngine;

public class TestInteractable : InteractableObject
{
    // Attributes
    public TestInteractableAnimationController animationController;

    // Constructor
    protected new void Start()
    {
        base.Start();
        animationController.Init(this);
    }

    // Function
    public override void Interact()
    {
        Debug.Log("Test Interactable interacted");
        if (!GameSaveManager.Instance.GetActiveGameSave().storyData.IsEventComplete(StoryConfig.KEY_STORY_1_START_CUTSCENE))
        {
            GameController.Instance.StartCutscene(StoryConfig.KEY_STORY_1_START_CUTSCENE);
            GameSaveManager.Instance.GetActiveGameSave().storyData.CompleteEvent(StoryConfig.KEY_STORY_1_START_CUTSCENE);
        }
        else if (!GameSaveManager.Instance.GetActiveGameSave().storyData.IsEventComplete(StoryConfig.KEY_STORY_1_END_CUTSCENE))
        {
            GameController.Instance.StartCutscene(StoryConfig.KEY_STORY_1_END_CUTSCENE);
            GameSaveManager.Instance.GetActiveGameSave().storyData.CompleteEvent(StoryConfig.KEY_STORY_1_END_CUTSCENE);
        }
        else if (!GameSaveManager.Instance.GetActiveGameSave().storyData.IsEventComplete(StoryConfig.KEY_STORY_2_END_CUTSCENE))
        {
            GameController.Instance.StartCutscene(StoryConfig.KEY_STORY_2_END_CUTSCENE);
            GameSaveManager.Instance.GetActiveGameSave().storyData.CompleteEvent(StoryConfig.KEY_STORY_2_END_CUTSCENE);
        }
        else if (!GameSaveManager.Instance.GetActiveGameSave().storyData.IsEventComplete(StoryConfig.KEY_STORY_3_END_CUTSCENE))
        {
            GameController.Instance.StartCutscene(StoryConfig.KEY_STORY_3_END_CUTSCENE);
            GameSaveManager.Instance.GetActiveGameSave().storyData.CompleteEvent(StoryConfig.KEY_STORY_3_END_CUTSCENE);
        }
        else if (!GameSaveManager.Instance.GetActiveGameSave().storyData.IsEventComplete(StoryConfig.KEY_STORY_ENDING_CUTSCENE))
        {
            GameController.Instance.StartCutscene(StoryConfig.KEY_STORY_ENDING_CUTSCENE);
            GameSaveManager.Instance.GetActiveGameSave().storyData.CompleteEvent(StoryConfig.KEY_STORY_ENDING_CUTSCENE);
        }
        else if (!GameSaveManager.Instance.GetActiveGameSave().storyData.IsEventComplete(StoryConfig.KEY_STORY_ENDING_AFTER_CUTSCENE))
        {
            GameController.Instance.StartCutscene(StoryConfig.KEY_STORY_ENDING_AFTER_CUTSCENE);
            GameSaveManager.Instance.GetActiveGameSave().storyData.CompleteEvent(StoryConfig.KEY_STORY_ENDING_AFTER_CUTSCENE);
        }
        else
        {
            GameController.Instance.StartCutscene(StoryConfig.KEY_CUTSCENE_DILUC);
        }
    }

    public override void OnInteractAreaEnter()
    {
        Debug.Log("Test Interactable is interactable now");
    }

    public override void OnInteractAreaExit()
    {
        Debug.Log("Test Interactable is uninteractable now");
    }
}