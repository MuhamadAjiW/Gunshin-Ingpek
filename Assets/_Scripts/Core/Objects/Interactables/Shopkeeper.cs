using UnityEngine;

namespace _Scripts.Core.Objects.Interactables
{
    public class Shopkeeper : InteractableObject
    {
        public bool active = true;
        public GameObject keeper;
        [HideInInspector] public Collider area;

        protected new void Start()
        {
            base.Start();
            area = GetComponent<Collider>();
            
            if(!active)
            {
                keeper.SetActive(false);
                area.enabled = false;
            }
        }

        public override void Interact()
        {
            // _TODO: Integrate with actual shop
            if(!active)
            {
                return;
            }
            GameController.Instance.StartCutscene(StoryConfig.KEY_CUTSCENE_SHOP);
            DialogController.Instance.OnCutsceneFinished += Shop;
        }

        public override void OnInteractAreaEnter()
        {
        }

        public override void OnInteractAreaExit()
        {
        }

        public void SetActive(bool active)
        {
            this.active = active;
            keeper.SetActive(active);
            area.enabled = active;
        }

        private void Shop()
        {
            DialogController.Instance.OnCutsceneFinished -= Shop;
            GameController.Instance.stateController.PushState(GameState.SHOPPING);
        }
    }
}