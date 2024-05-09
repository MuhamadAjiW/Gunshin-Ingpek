using System.Collections.Generic;
using System.Linq;

namespace _Scripts.Core.Objects.Interactables
{
    public class SavePoint : InteractableObject
    {
        public override void Interact()
        {
            // _TODO: Integrate with actual saving
            GameController.Instance.player.Health = GameController.Instance.player.MaxHealth;
            GameController.Instance.StartCutscene(StoryConfig.KEY_CUTSCENE_SAVE);
            GameSaveManager.Instance.gameSaves[GameSaveManager.Instance.activeGameSaveIndex].positionData.point = GameController.Instance.player.transform.position;
            GameSaveManager.Instance.gameSaves[GameSaveManager.Instance.activeGameSaveIndex].weaponPoolIndex =
                new List<int>(GameController.Instance.player.weaponList.Where(e => e.poolIndex != 0).Select(
                (e) => e.poolIndex));
            GameSaveManager.Instance.PersistActiveSave();
        }

        public override void OnInteractAreaEnter()
        {
        }

        public override void OnInteractAreaExit()
        {
        }

    }
}