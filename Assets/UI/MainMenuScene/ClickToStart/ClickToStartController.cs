using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class ClickToStartController : MonoBehaviour
{
    [SerializeField] public UIDocument ClickToStartUIDocument;
    [SerializeField] public MainMenuManager MainMenuManager;
    private VisualElement rootElement;

    public void OnEnable()
    {
        rootElement = ClickToStartUIDocument.rootVisualElement;
    }

    public void Update()
    {
        if (MainMenuManager.OpenedUIDocumentString == MainMenuManager.GetDocumentName(ClickToStartUIDocument) && Input.anyKey)
        {
            if (!MainMenuManager.MainMenuTransitionInProgress)
            {
                MainMenuManager.DisplayUIDocument("MainMenu", true);
            }
        }
    }
}
