using System.Collections;
using System.Collections.Generic;
using _Scripts.Core.Game.Data;
using UnityEngine;

public class Level3 : MonoBehaviour
{
    public int goonValue = 1;
    public int headGoonValue = 5;
    public int generalValue = 20;
    public int kingValue = 100;
    public int killLimit = 100;

    private int killCounter = 0;
    public GameObject enemySpawners;

    protected void Start()
    {
        enemySpawners = transform.Find("EnemySpawners").gameObject;
        if(!GameSaveManager.Instance.GetActiveGameSave().storyData.IsEventComplete(StoryConfig.KEY_STORY_3_START_CUTSCENE))
        {
            GameController.Instance.OnGameEvent += OnGameEvent;
        }
        
        if (enemySpawners != null)
        {
            enemySpawners.SetActive(false);
        }
    }

    public void OnGameEvent(string eventId, System.Object info)
    {
        if(eventId == StoryConfig.KEY_STORY_3_START_CUTSCENE)
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
            if(info is Goon)
            {
                killCounter += goonValue;                
            }
            else if(info is HeadGoon)
            {
                killCounter += headGoonValue;
            }
            else if(info is General)
            {
                killCounter += generalValue;
            }
            else if(info is King)
            {
                killCounter += kingValue;
            }

            if(killCounter >= killLimit)
            {
                GameController.Instance.OnGameEvent -= CountDeaths;
                GameController.Instance.StartCutscene(StoryConfig.KEY_STORY_3_END_CUTSCENE);
<<<<<<< HEAD:Assets/_Scripts/Core/Levels/Level3.cs
                GameSaveData.Instance.storyData.CompleteEvent(StoryConfig.KEY_STORY_3_END_CUTSCENE);
                DialogController.Instance.OnCutsceneFinished += EndLevel;
=======
                GameSaveManager.Instance.GetActiveGameSave().storyData.CompleteEvent(StoryConfig.KEY_STORY_3_END_CUTSCENE);

                if (enemySpawners != null)
                {
                    enemySpawners.SetActive(false);
                }
>>>>>>> da7b0d1c (feat: level 2 and 3):Assets/_Scripts/Core/Levels/Level3/Level3.cs
            }
        }
    }
}
