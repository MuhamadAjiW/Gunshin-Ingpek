using System;
using System.Collections.Generic;
using _Scripts.Core.Game.Data.Saves;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class SavesGameContainer : VisualElement
{
    public static readonly string GameSavesContainerUSSClassName = "saves-game-container";
    public static readonly string GameSaveUSSClassName = "save-game";

    private VisualElement m_GameSavesContainer;

    public List<GameSaveData> Saves
    {
        get => GameSaveManager.Instance.GetSaves();
    }

    public SavesGameContainer()
    {

        m_GameSavesContainer = new VisualElement
        {
            name = "GameSavesContainer"
        };
        m_GameSavesContainer.AddToClassList(GameSavesContainerUSSClassName);
        Add(m_GameSavesContainer);

        generateVisualContent += GenerateVisualContent;

        foreach (GameSaveData save in Saves)
        {
            // Add GameSave for each save data

        }

    }

    void GenerateVisualContent(MeshGenerationContext context)
    {
    }
}