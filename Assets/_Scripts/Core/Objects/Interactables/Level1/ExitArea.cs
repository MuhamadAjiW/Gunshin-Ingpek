using System.Collections;
using System.Collections.Generic;
using _Scripts.Core.Game.Data;
using UnityEngine;

public class ExitInteractable_LV1 : InteractableObject
{
    public override void Interact()
    {
    }

    public override void OnInteractAreaEnter()
    {
        // TODO: Proceed to next level
        GameController.Instance.StartCutscene(StoryConfig.KEY_STORY_1_END_CUTSCENE);
        GameSaveData.Instance.storyData.CompleteEvent(StoryConfig.KEY_STORY_1_END_CUTSCENE);
        DialogController.Instance.OnCutsceneFinished += EndLevel;
    }

    public override void OnInteractAreaExit()
    {
    }

    public void EndLevel()
    {
        DialogController.Instance.OnCutsceneFinished -= EndLevel;
        GameController.Instance.stateController.PopState();
        GameController.Instance.stateController.PushState(GameState.FINISH);
    }
}
