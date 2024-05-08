using System.Collections;
using System.Collections.Generic;
using _Scripts.Core.Game.Data;
using UnityEngine;

public class Level4 : MonoBehaviour
{
    protected void Start()
    {
        GameController.Instance.OnGameEvent += CountDeaths;
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
        // TODO: Proceed to next level
        DialogController.Instance.OnCutsceneFinished -= EndLevel;
        GameController.Instance.stateController.PopState();
        GameController.Instance.stateController.PushState(GameState.FINISH);
    }
}
