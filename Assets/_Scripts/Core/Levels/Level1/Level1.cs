using System.Collections;
using System.Collections.Generic;
using _Scripts.Core.Game.Data;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    public static float SHOP_TIMEOUT = 30f;

    protected void Start()
    {
        if(!GameSaveManager.Instance.GetActiveGameSave().storyData.IsEventComplete(StoryConfig.KEY_STORY_1_START_CUTSCENE))
        {
            GameController.Instance.StartCutscene(StoryConfig.KEY_STORY_1_START_CUTSCENE);
            GameSaveManager.Instance.GetActiveGameSave().storyData.CompleteEvent(StoryConfig.KEY_STORY_1_START_CUTSCENE);
        }
    }
}
