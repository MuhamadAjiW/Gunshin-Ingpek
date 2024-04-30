using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingsController : MonoBehaviour
{
    [SerializeField] public UIDocument SettingsUIDocument;
    [SerializeField] public MainMenuManager MainMenuManager;

    private VisualElement rootElement;
    public void OnEnable()
    {
        rootElement = SettingsUIDocument.rootVisualElement;

        MainMenuManager.InitializeBackButton(rootElement);

    }
}
