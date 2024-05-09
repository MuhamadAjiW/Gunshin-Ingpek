namespace _Scripts.Core.Objects.Interactables
{
    public class SavePoint : InteractableObject
    {
        public override void Interact()
        {
            // _TODO: Integrate with actual saving
            GameController.Instance.StartCutscene(StoryConfig.KEY_CUTSCENE_SAVE);
        }

        public override void OnInteractAreaEnter()
        {
        }

        public override void OnInteractAreaExit()
        {
        }

    }
}