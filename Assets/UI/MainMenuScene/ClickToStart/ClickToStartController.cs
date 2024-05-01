using UnityEngine;

public class ClickToStartController : MainMenuScreenController
{
    public new void OnEnable()
    {
        base.OnEnable();
    }

    public void Update()
    {
        if (IsThisUIDocumentOpened && Input.anyKey)
        {
            if (!MainMenuManager.MainMenuTransitionInProgress)
            {
                MainMenuManager.DisplayUIDocument("MainMenu", true);
            }
        }
    }
}
