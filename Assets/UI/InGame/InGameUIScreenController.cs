using UnityEngine;
public class InGameUIScreenController : ScreenController
{
    [SerializeField] public InGameUIManager InGameUIManager;

    protected string ControlledUIDocumentName
    {
        get => UIManagement.GetDocumentName(ControlledUIDocument);
    }

    protected bool IsThisUIDocumentOpened
    {
        get => InGameUIManager.CheckIfDocumentIsOpened(ControlledUIDocumentName);
    }
    public new void OnEnable()
    {
        base.OnEnable();
    }

}