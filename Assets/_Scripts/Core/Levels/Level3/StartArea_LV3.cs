using System.Collections;
using System.Collections.Generic;
using _Scripts.Core.Game.Data;
using UnityEngine;

public class StartInteractable_LV3 : InteractableObject
{
    public override void Interact()
    {
    }

    public override void OnInteractAreaEnter()
    {
        if (!GameSaveManager.Instance.GetActiveGameSave().storyData.IsEventComplete(StoryConfig.KEY_STORY_3_START_CUTSCENE))
        {
            GameController.Instance.StartCutscene(StoryConfig.KEY_STORY_3_START_CUTSCENE);
            GameSaveManager.Instance.GetActiveGameSave().storyData.CompleteEvent(StoryConfig.KEY_STORY_3_START_CUTSCENE);
        }
    }

    public override void OnInteractAreaExit()
    {
    }
}
