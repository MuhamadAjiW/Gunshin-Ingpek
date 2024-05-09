using System.Collections;
using System.Collections.Generic;
using _Scripts.Core.Game.Data;
using UnityEngine;

public class StartInteractable_LV4 : InteractableObject
{
    public Transform teleportLocation;
    public override void Interact()
    {
        if (!GameSaveManager.Instance.GetActiveGameSave().storyData.IsEventComplete(StoryConfig.KEY_STORY_4_START_CUTSCENE))
        {
            GameController.Instance.StartCutscene(StoryConfig.KEY_STORY_4_START_CUTSCENE);
            GameSaveManager.Instance.GetActiveGameSave().storyData.CompleteEvent(StoryConfig.KEY_STORY_4_START_CUTSCENE);
        }
        
        GameController.Instance.player.transform.SetPositionAndRotation(teleportLocation.position, teleportLocation.rotation);
        GameController.Instance.mainCamera.activeCamera.transform.SetPositionAndRotation(teleportLocation.position, teleportLocation.rotation);
        GameController.Instance.mainCamera.SetCameraBehaviour(CameraBehaviourType.MOUSE);
        (GameController.Instance.mainCamera.behaviour as CameraFollowObject).target = GameController.Instance.player.transform;
    }

    public override void OnInteractAreaEnter()
    {
    }

    public override void OnInteractAreaExit()
    {
    }
}
