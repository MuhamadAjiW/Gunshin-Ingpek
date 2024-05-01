using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuScreenController : MonoBehaviour
{
    [SerializeField] public UIDocument ControlledUIDocument;
    [SerializeField] public MainMenuManager MainMenuManager;

    protected VisualElement rootElement;

    protected string ControlledUIDocumentName
    {
        get => MainMenuManager.GetDocumentName(ControlledUIDocument);
    }

    protected bool IsThisUIDocumentOpened
    {
        get => MainMenuManager.OpenedUIDocumentString == ControlledUIDocumentName;
    }
    public void OnEnable()
    {
        rootElement = ControlledUIDocument.rootVisualElement;
    }


}
