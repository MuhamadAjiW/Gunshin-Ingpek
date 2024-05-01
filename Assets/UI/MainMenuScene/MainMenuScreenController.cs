using UnityEngine;

public class MainMenuScreenController : ScreenController
{
    [SerializeField] public MainMenuManager MainMenuManager;

    protected string ControlledUIDocumentName
    {
        get => UIManagement.GetDocumentName(ControlledUIDocument);
    }

    protected bool IsThisUIDocumentOpened
    {
        get => MainMenuManager.OpenedUIDocumentString == ControlledUIDocumentName;
    }
    public new void OnEnable()
    {
        base.OnEnable();
    }


}
