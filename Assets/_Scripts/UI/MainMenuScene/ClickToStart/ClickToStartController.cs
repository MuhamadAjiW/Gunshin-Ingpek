using UnityEngine;

public class ClickToStartController : MainMenuScreenController
{
    public new void Start()
    {
        base.Start();
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
