using UnityEngine;
public class InGameUIScreenController : ScreenController
{
    public GameController GameController;

    public Player player;
    [SerializeField] public InGameUIManager InGameUIManager;

    protected string ControlledUIDocumentName
    {
        get => UIManagement.GetDocumentName(ControlledUIDocument);
    }

    protected bool IsThisUIDocumentOpened
    {
        get => InGameUIManager.CheckIfDocumentIsOpened(ControlledUIDocumentName);
    }
    public new void Start()
    {
        base.Start();
    }

}