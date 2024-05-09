using System.Collections;
using System.Collections.Generic;
using _Scripts.Core.Game.Data;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    public GameObject enemySpawners;

    protected void Start()
    {
        enemySpawners = transform.Find("EnemySpawners").gameObject;
        if(!GameSaveManager.Instance.GetActiveGameSave().storyData.IsEventComplete(StoryConfig.KEY_STORY_1_ENTER_DUNGEON))
        {
            GameController.Instance.OnGameEvent += OnGameEvent;
            
            if (enemySpawners != null)
            {
                enemySpawners.SetActive(false);
            }
        }
        else
        {
            if (enemySpawners != null)
            {
                enemySpawners.SetActive(true);
            }
        }
    }

    public void OnGameEvent(string eventId, System.Object info)
    {
        if(eventId == StoryConfig.KEY_STORY_1_ENTER_DUNGEON)
        {
            GameController.Instance.OnGameEvent -= OnGameEvent;
        
            if (enemySpawners != null)
            {
                enemySpawners.SetActive(true);
            }
        }
    }
}
