using System.Collections;
using System.Collections.Generic;
using _Scripts.Core.Game.Data;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    public static float SHOP_TIMEOUT = 60f;

    protected void Start()
    {
        if(!GameSaveManager.Instance.GetActiveGameSave().storyData.IsEventComplete(StoryConfig.KEY_STORY_1_START_CUTSCENE))
        {
            GameController.Instance.player.weaponList.Add(EventManager.Instance.WeaponPool[1]);

            GameController.Instance.StartCutscene(StoryConfig.KEY_STORY_1_START_CUTSCENE);
            GameSaveManager.Instance.GetActiveGameSave().storyData.CompleteEvent(StoryConfig.KEY_STORY_1_START_CUTSCENE);

            // _TODO: Add guard to detect whether the player has finished the game before
            if(GameSaveManager.Instance.GetActiveGameSave().difficulty != DifficultyType.HARD)
            {
                DialogController.Instance.OnCutsceneFinished += Tutorial;
                GameController.Instance.OnGameEvent += OnDummyDeath;
            }
            else
            {
                GameSaveManager.Instance.GetActiveGameSave().storyData.CompleteEvent(StoryConfig.KEY_TUTORIAL_FINAL);
                GameController.Instance.player.weaponList.Add(EventManager.Instance.WeaponPool[4]);
                
                Debug.Log("Logging weapon indices");
                foreach (WeaponObject weapons in GameController.Instance.player.weaponList)
                {
                    Debug.Log(weapons.poolIndex);
                }
            }
        }
    }

    public void Tutorial()
    {
        DialogController.Instance.OnCutsceneFinished -= Tutorial;
        GameController.Instance.StartCutscene(StoryConfig.KEY_TUTORIAL_START);
        GameSaveManager.Instance.GetActiveGameSave().storyData.CompleteEvent(StoryConfig.KEY_TUTORIAL_START);
    }

    public void OnDummyDeath(string eventId, System.Object info)
    {
        if(eventId == GameConfig.EVENT_ENEMY_KILLED && info is Dummy)
        {
            GameController.Instance.OnGameEvent -= OnDummyDeath;
            GameSaveManager.Instance.GetActiveGameSave().storyData.CompleteEvent(StoryConfig.KEY_TUTORIAL_2);
            return;
        }

        if (eventId == StoryConfig.KEY_STORY_1_ENTER_DUNGEON)
        {
            GameController.Instance.OnGameEvent -= OnDummyDeath;
            return;
        }
    }
}
