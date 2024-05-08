using System.Collections;
using System.Collections.Generic;
using _Scripts.Core.Game.Data;
using UnityEngine;

public class Diluc : InteractableObject
{
    public override void Interact()
    {
        GameController.Instance.StartCutscene(StoryConfig.KEY_CUTSCENE_DILUC);
    }

    public override void OnInteractAreaEnter()
    {
    }

    public override void OnInteractAreaExit()
    {
    }
}
