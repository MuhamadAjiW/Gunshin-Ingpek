using System;
using System.Collections.Generic;
using _Scripts.Core.Game.Data.Saves;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class GameStatisticsContainer : VisualElement
{


    public static readonly string GameStatisticsContainerUSSClassName = "game-statistics-container";

    private VisualElement m_GameStatisticsContainer;
    public List<GameSaveData> Saves
    {
        get => GameSaveManager.Instance.GetSaves();
    }

    public GameStatisticsContainer()
    {
        m_GameStatisticsContainer = new VisualElement
        {
            name = "GameStatisticsContainer"
        };
        m_GameStatisticsContainer.AddToClassList(GameStatisticsContainerUSSClassName);

        generateVisualContent += GenerateVisualContent;

    }

    void GenerateVisualContent(MeshGenerationContext context)
    {
    }
}