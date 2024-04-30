using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LoadGameController : MonoBehaviour
{
    [SerializeField] public UIDocument LoadGameUIDocument;
    [SerializeField] public MainMenuManager MainMenuManager;

    private VisualElement rootElement;
    public void OnEnable()
    {
        rootElement = LoadGameUIDocument.rootVisualElement;

        MainMenuManager.InitializeBackButton(rootElement);



    }
}
