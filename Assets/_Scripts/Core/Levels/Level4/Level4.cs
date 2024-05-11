using System.Collections;
using System.Collections.Generic;
using _Scripts.Core.Game.Data;
using UnityEngine;

public class Level4 : MonoBehaviour
{
   public GameObject enemySpawners;
    public GameObject mobs;

    protected void Start()
    {
        enemySpawners = transform.Find("EnemySpawners").gameObject;
        mobs = transform.Find("Mobs").gameObject;
        if(!GameSaveManager.Instance.GetActiveGameSave().storyData.IsEventComplete(StoryConfig.KEY_STORY_4_START_CUTSCENE))
        {
            GameController.Instance.OnGameEvent += OnGameEvent;
        }
        
        if (enemySpawners != null)
        {
            enemySpawners.SetActive(false);
        }
        if (mobs != null)
        {
            mobs.SetActive(false);
        }
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
            if (mobs != null)
            {
                mobs.SetActive(true);
                
                Transform[] childTransforms = new Transform[mobs.transform.childCount];
                for (int i = 0; i < mobs.transform.childCount; i++)
                {
                    childTransforms[i] = mobs.transform.GetChild(i);
                }

                // Reparent the children to anotherGameObject
                foreach (Transform childTransform in childTransforms)
                {
                    childTransform.SetParent(EntityManager.Instance.transform);
                }
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
<<<<<<< HEAD
                GameController.Instance.StartCutscene(StoryConfig.KEY_STORY_ENDING_CUTSCENE);
                GameSaveData.Instance.storyData.CompleteEvent(StoryConfig.KEY_STORY_ENDING_CUTSCENE);
                DialogController.Instance.OnCutsceneFinished += EndLevel;
=======
                GameController.Instance.StartCutscene(StoryConfig.KEY_STORY_ENDING_CUTSCENE, EndLevel);
                GameSaveManager.Instance.GetActiveGameSave().storyData.CompleteEvent(StoryConfig.KEY_STORY_ENDING_CUTSCENE);
>>>>>>> e57a0f1c (refactor: improved dialog callbacks)
            }
        }
    }

    public void EndLevel()
    {
        GameAudioController.Instance.audioController.Stop("ost");
        GameController.Instance.StartCutscene(StoryConfig.KEY_STORY_ENDING_AFTER_CUTSCENE, EndGame);
        GameSaveManager.Instance.GetActiveGameSave().storyData.CompleteEvent(StoryConfig.KEY_STORY_ENDING_AFTER_CUTSCENE);
    }

    public void EndGame()
    {
        GameController.Instance.stateController.PopState();
        GameController.Instance.stateController.PushState(GameState.FINISH);
    }
}
