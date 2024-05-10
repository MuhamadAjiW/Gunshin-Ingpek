using System.Collections.Generic;
using System.Linq;

namespace _Scripts.Core.Objects.Interactables
{
    public class SavePoint : InteractableObject
    {
        public override void Interact()
        {
            // _TODO: Integrate with actual saving
            GameSaveManager.Instance.SaveGame();
        }

        public override void OnInteractAreaEnter()
        {
        }

        public override void OnInteractAreaExit()
        {
        }

    }
}