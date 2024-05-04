using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class InGameUIManager : MonoBehaviour
{
    public List<UIDocument> InGameUIDocuments;
    public CameraManager CameraManager;

    public GameController GameController;

    private readonly List<UIDocument> OpenUIDocuments = new();

    public Player player;

    public void Awake()
    {
        GameController.stateController.OnPausedEvent += (bool pause) =>
        {
            ToggleUIDocumentVisibility("Crosshair", !pause);
            ToggleUIDocumentVisibility("PauseMenu", pause);
        };
    }

    public void OnEnable()
    {
        ToggleUIDocumentVisibility("PauseMenu", false);
    }


    public bool CheckIfDocumentIsOpened(string documentName)
    {
        return OpenUIDocuments.Select(document => UIManagement.GetDocumentName(document)).Any(name => name == documentName);
    }

    public void ToggleUIDocumentVisibility(string documentName, bool isVisible)
    {
        UIDocument uiDocument = InGameUIDocuments.Single(document => UIManagement.GetDocumentName(document) == documentName);

        if (isVisible && OpenUIDocuments.SingleOrDefault(document => UIManagement.GetDocumentName(document) == documentName) is null)
        {
            UIManagement.ToggleUIDocumentVisible(uiDocument);
        }

        if (!isVisible)
        {
            UIManagement.ToggleUIDocumentVisible(uiDocument, false);
            var index = OpenUIDocuments.FindIndex(document => UIManagement.GetDocumentName(document) == documentName);
            if (index != -1)
            {
                OpenUIDocuments.RemoveAt(index);
            }
        }
    }
}