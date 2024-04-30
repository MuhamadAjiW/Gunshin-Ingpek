using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameStatisticsDisplayController : MonoBehaviour
{
    [SerializeField] public UIDocument GameStatisticsDisplayUIDocument;
    [SerializeField] public MainMenuManager MainMenuManager;

    private VisualElement rootElement;
    public void OnEnable()
    {
        rootElement = GameStatisticsDisplayUIDocument.rootVisualElement;

        MainMenuManager.InitializeBackButton(rootElement);


    }
}
