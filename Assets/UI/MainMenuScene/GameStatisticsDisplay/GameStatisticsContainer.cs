using System;
using System.Collections.Generic;
using _Scripts.Core.Game.Data;
using _Scripts.Core.Game.Data.Saves;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

[UxmlElement]
public partial class GameStatisticsContainer : VisualElement
{
    public static readonly string GameStatisticsContainerUSSClassName = "game-statistics-container";


    private VisualElement m_EnemiesKilledContainer = new();

    private TextElement m_EnemiesKilledText = new();

    private VisualElement m_PlaytimeContainer = new();
    private TextElement m_PlaytimeText = new();

    private VisualElement m_DistanceTraveledContainer = new();
    private TextElement m_DistanceTraveledText = new();

    private VisualElement m_ShotsContainer = new();

    private VisualElement m_ShotsFiredContainer = new();
    private VisualElement m_ShotsHitContainer = new();

    private VisualElement m_AccuracyContainer = new();

    private TextElement m_ShotsFiredText = new();
    private TextElement m_ShotsHitText = new();
    private TextElement m_AccuracyText = new();


    // Statistics
    public int EnemiesKilled
    {
        get => GameStatisticsManager.Instance.EnemiesKilled;
    }

    public int GoonsKilled
    {
        get => GameStatisticsManager.Instance.goonsKilled;
    }

    public int HeadGoonsKilled
    {
        get => GameStatisticsManager.Instance.headgoonsKilled;
    }

    public int GeneralsKilled
    {
        get => GameStatisticsManager.Instance.generalsKilled;
    }

    public int KingsKilled
    {
        get => GameStatisticsManager.Instance.kingsKilled;
    }

    public int ShotsFired
    {
        get => GameStatisticsManager.Instance.shotsFired;
    }

    public int ShotsHit
    {
        get => GameStatisticsManager.Instance.shotsHit;
    }


    public float ShotsAccuracy
    {
        get => GameStatisticsManager.Instance.Accuracy;
    }

    public GameStatisticsContainer()
    {
        name = "GameStatisticsContainer";
        AddToClassList(GameStatisticsContainerUSSClassName);

        // Setup enemies killed statistics
        m_EnemiesKilledText.text = EnemiesKilled.ToString();
        m_EnemiesKilledContainer.Add(m_EnemiesKilledText);

        // Setup shots statistics
        m_ShotsFiredText.text = ShotsFired.ToString();
        m_ShotsHitText.text = ShotsHit.ToString();
        m_AccuracyText.text = ShotsAccuracy.ToString();

        m_ShotsFiredContainer.Add(m_ShotsFiredText);
        m_ShotsHitContainer.Add(m_ShotsHitText);
        m_AccuracyContainer.Add(m_AccuracyText);

        m_ShotsContainer.Add(m_ShotsFiredContainer);
        m_ShotsContainer.Add(m_ShotsHitContainer);
        m_ShotsContainer.Add(m_AccuracyContainer);

        generateVisualContent += GenerateVisualContent;

    }

    void GenerateVisualContent(MeshGenerationContext context)
    {
    }
}