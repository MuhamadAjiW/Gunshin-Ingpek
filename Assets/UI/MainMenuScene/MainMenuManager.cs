using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] public List<UIDocument> MainMenuUIDocuments;

    private UIDocument OpenedUIDocument;

    public void OnEnable()
    {
        GoToMainMenu();
    }

    public void InitializeBackButton(VisualElement rootElement)
    {
        Button backButton = rootElement.Query<Button>("back-button");
        if (backButton is null)
        {
            return;
        }

        backButton.RegisterCallback<ClickEvent>((ClickEvent evt) =>
        {
            GoToMainMenu();
        });
    }

    public void GoToMainMenu()
    {
        DisplayUIDocument("MainMenu");
    }

    public void DisplayUIDocument(string displayedUIDocumentName)
    {
        foreach (var uiDocument in MainMenuUIDocuments)
        {
            if (uiDocument.ToString().Split(" ")[0] != displayedUIDocumentName)
            {
                OpenedUIDocument = uiDocument;
                uiDocument.rootVisualElement.style.display = DisplayStyle.None;
            }
            else
            {
                uiDocument.rootVisualElement.style.display = DisplayStyle.Flex;
            }
        }
    }



}
