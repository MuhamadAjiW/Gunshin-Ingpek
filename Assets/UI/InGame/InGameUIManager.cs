using System;
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
        HideAllDocuments();
        HandleGameStateChange(GameController.stateController.GetState());
        GameController.stateController.OnGameStateChange += (GameStateChangeArgs e) => HandleGameStateChange(e.NewGameState);
    }

    public void HideAllDocuments()
    {
        InGameUIDocuments.ForEach((document) => ToggleUIDocumentVisibility(UIManagement.GetDocumentName(document), false));
    }

    public void HandleGameStateChange(GameState gameState)
    {
        SetUIDocumentVisibleOnThisState("Crosshair", new List<GameState> { GameState.RUNNING })(gameState);
        SetUIDocumentVisibleOnThisState("PauseMenu", new List<GameState> { GameState.PAUSED })(gameState);
        SetUIDocumentVisibleOnThisState("GameOverScreen", new List<GameState> { GameState.OVER })(gameState);
        SetUIDocumentVisibleOnThisState("CompletedScreen", new List<GameState> { GameState.FINISH })(gameState);
        SetUIDocumentVisibleOnThisState("HealthBar", new List<GameState> { GameState.RUNNING })(gameState);

    }

    public void OnEnable()
    {
        Debug.Log("In Game UI Documents");
        foreach (UIDocument document in InGameUIDocuments)
        {
            Debug.Log(UIManagement.GetDocumentName(document));
        }
    }

    public EventCallback<GameState> SetUIDocumentVisibleOnThisState(string documentName, List<GameState> gameStateWhenDocumentVisible)
    {
        return (GameState gameState) =>
        {
            ToggleUIDocumentVisibility(documentName, gameStateWhenDocumentVisible.Contains(gameState));
        };

    }


    public bool CheckIfDocumentIsOpened(string documentName)
    {
        return OpenUIDocuments.Select(document => UIManagement.GetDocumentName(document)).Any(name => name == documentName);
    }

    public void ToggleUIDocumentVisibility(string documentName, bool isVisible)
    {
        Debug.Log(String.Format("{0}: {1}", documentName, isVisible));
        UIDocument uiDocument = InGameUIDocuments.SingleOrDefault(document => UIManagement.GetDocumentName(document) == documentName);

        if (uiDocument is null)
        {
            Debug.LogError(String.Format("Toggling non-existent ui document {0}", documentName));
            return;
        }

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