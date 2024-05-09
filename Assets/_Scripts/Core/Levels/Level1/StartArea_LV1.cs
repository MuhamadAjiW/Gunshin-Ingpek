using System.Collections;
using System.Collections.Generic;
using _Scripts.Core.Game.Data;
using UnityEngine;

public class StartInteractable_LV1 : InteractableObject
{
    public override void Interact()
    {
    }

    public override void OnInteractAreaEnter()
    {
        if(!GameSaveData.Instance.storyData.IsEventComplete(StoryConfig.KEY_STORY_1_ENTER_DUNGEON))
        {
            GameSaveData.Instance.storyData.CompleteEvent(StoryConfig.KEY_STORY_1_ENTER_DUNGEON);
        }
    }

    public override void OnInteractAreaExit()
    {
    }
}
