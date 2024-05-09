using System.Collections;
using System.Collections.Generic;
using _Scripts.Core.Game.Data;
using UnityEngine;

public class Level4 : MonoBehaviour
{
   public GameObject enemySpawners;

    protected void Start()
    {
        // enemySpawners = transform.Find("EnemySpawners").gameObject;
        if(!GameSaveManager.Instance.GetActiveGameSave().storyData.IsEventComplete(StoryConfig.KEY_STORY_4_START_CUTSCENE))
        {
            GameController.Instance.OnGameEvent += OnGameEvent;
        }
        
        // if (enemySpawners != null)
        // {
            // enemySpawners.SetActive(false);
        // }
    }

    public void OnGameEvent(string eventId, System.Object info)
    {
        if(eventId == StoryConfig.KEY_STORY_4_START_CUTSCENE)
        {
            GameController.Instance.OnGameEvent -= OnGameEvent;
            GameController.Instance.OnGameEvent += CountDeaths;
        
            if (enemySpawners != null)
            {
                enemySpawners.SetActive(true);
            }
        }
    }

    private void CountDeaths(string eventId, System.Object info)
    {
        if(eventId == GameConfig.EVENT_ENEMY_KILLED)
        {
            if(info is King)
            {
                GameController.Instance.OnGameEvent -= CountDeaths;
                GameController.Instance.StartCutscene(StoryConfig.KEY_STORY_ENDING_CUTSCENE);
                GameSaveData.Instance.storyData.CompleteEvent(StoryConfig.KEY_STORY_ENDING_CUTSCENE);
                DialogController.Instance.OnCutsceneFinished += EndLevel;
            }
        }
    }

    public void EndLevel()
    {
        DialogController.Instance.OnCutsceneFinished -= EndLevel;
        GameController.Instance.StartCutscene(StoryConfig.KEY_STORY_ENDING_AFTER_CUTSCENE);
        GameSaveManager.Instance.GetActiveGameSave().storyData.CompleteEvent(StoryConfig.KEY_STORY_ENDING_AFTER_CUTSCENE);
        DialogController.Instance.OnCutsceneFinished += EndGame;
    }

    public void EndGame()
    {
        DialogController.Instance.OnCutsceneFinished -= EndGame;
        GameController.Instance.stateController.PopState();
        GameController.Instance.stateController.PushState(GameState.FINISH);
    }
}
