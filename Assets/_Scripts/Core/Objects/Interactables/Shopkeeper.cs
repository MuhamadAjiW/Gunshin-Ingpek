namespace _Scripts.Core.Objects.Interactables
{
    public class Shopkeeper : InteractableObject
    {
        public override void Interact()
        {
            // _TODO: Integrate with actual shop
            GameController.Instance.StartCutscene(StoryConfig.KEY_CUTSCENE_SHOP);
        }

        public override void OnInteractAreaEnter()
        {
        }

        public override void OnInteractAreaExit()
        {
        }

    }
}