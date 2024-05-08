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

    protected void Start()
    {
        GameController.Instance.OnGameEvent += CountDeaths;
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
                GameSaveData.Instance.storyData.CompleteEvent(StoryConfig.KEY_STORY_3_END_CUTSCENE);
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
