namespace _Scripts.Core.Objects.Interactables
{
    public class SavePoint : InteractableObject
    {
        public override void Interact()
        {
            // _TODO: Integrate with actual saving
            GameController.Instance.StartCutscene(StoryConfig.KEY_CUTSCENE_SAVE);
            GameSaveManager.Instance.gameSaves[GameSaveManager.Instance.activeGameSaveIndex].positionData.point = GameController.Instance.player.transform.position;
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