using System.Collections;
using System.Collections.Generic;
using _Scripts.Core.Game.Data;
using UnityEngine;

public class Diluc : InteractableObject
{
    public override void Interact()
    {
        if(GameSaveManager.Instance.GetActiveGameSave().storyData.IsEventComplete(StoryConfig.KEY_TUTORIAL_START) && !GameSaveManager.Instance.GetActiveGameSave().storyData.IsEventComplete(StoryConfig.KEY_STORY_1_ENTER_DUNGEON))
        {
            if(!GameSaveManager.Instance.GetActiveGameSave().storyData.IsEventComplete(StoryConfig.KEY_TUTORIAL_1))
            {
                GameController.Instance.StartCutscene(StoryConfig.KEY_TUTORIAL_1);
                GameSaveManager.Instance.GetActiveGameSave().storyData.CompleteEvent(StoryConfig.KEY_TUTORIAL_1);
                return;
            }
            if(!GameSaveManager.Instance.GetActiveGameSave().storyData.IsEventComplete(StoryConfig.KEY_TUTORIAL_2))
            {
                GameController.Instance.StartCutscene(StoryConfig.KEY_TUTORIAL_2);
                return;
            }
            if(!GameSaveManager.Instance.GetActiveGameSave().storyData.IsEventComplete(StoryConfig.KEY_TUTORIAL_FINAL))
            {
                GameController.Instance.StartCutscene(StoryConfig.KEY_TUTORIAL_FINAL);
                GameSaveManager.Instance.GetActiveGameSave().storyData.CompleteEvent(StoryConfig.KEY_TUTORIAL_FINAL);
                GameController.Instance.player.weaponList.Add(EventManager.Instance.WeaponPool[3]);
                return;
            }
        }

        GameController.Instance.StartCutscene(StoryConfig.KEY_CUTSCENE_DILUC);
    }

    public override void OnInteractAreaEnter()
    {
    }

    public override void OnInteractAreaExit()
    {
    }
}
