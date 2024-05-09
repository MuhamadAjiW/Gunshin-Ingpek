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
        if (!GameSaveManager.Instance.GetActiveGameSave().storyData.IsEventComplete(StoryConfig.KEY_STORY_1_END_CUTSCENE))
        {
            GameController.Instance.StartCutscene(StoryConfig.KEY_STORY_1_END_CUTSCENE);
            GameSaveManager.Instance.GetActiveGameSave().storyData.CompleteEvent(StoryConfig.KEY_STORY_1_END_CUTSCENE);
            GameSaveManager.Instance.GetActiveGameSave().currencyData.AddTransaction(Level1.QUEST_REWARD, Level1.QUEST_NAME);
            EventManager.Instance.SetShop(0, true);
            StartCoroutine(ShopTimeout());
        }
    }

    public override void OnInteractAreaExit()
    {
    }

    private IEnumerator ShopTimeout()
    {
        yield return new WaitForSeconds(Level1.SHOP_TIMEOUT);
        EventManager.Instance.SetShop(0, false);
    }
}
