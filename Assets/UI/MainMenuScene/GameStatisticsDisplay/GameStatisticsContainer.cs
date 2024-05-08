using System;
using System.Collections.Generic;
using _Scripts.Core.Game.Data;

using Unity.Properties;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

[UxmlElement]
public partial class GameStatisticsContainer : VisualElement
{

    public static readonly string GameStatisticsContainerUSSClassName = "game-statistics-container";

    public static readonly string EnemiesKilledContainerUSSClassName = "enemies-killed-container";
    public static readonly string EnemiesKilledTextUSSClassName = "enemies-killed-text";

    private VisualElementWithClassAndName m_EnemiesKilledContainer = new("EnemiesKilledContainer", new List<string> { "statistics-container", EnemiesKilledContainerUSSClassName });

    private TextElementWithClassAndName m_EnemiesKilledText = new("EnemiesKilledText", new List<string> { EnemiesKilledTextUSSClassName, "statistics-text" });

    public static readonly string PlaytimeContainerUSSClassName = "playtime-container";
    public static readonly string PlaytimeTextUSSClassName = "playtime-text";
    private VisualElementWithClassAndName m_PlaytimeContainer = new("PlaytimeContainer", new List<string> { "statistics-container", PlaytimeContainerUSSClassName });
    private TextElementWithClassAndName m_PlaytimeText = new("PlaytimeText", new List<string> { PlaytimeTextUSSClassName, "statistics-text" });

    public static readonly string DistanceTraveledContainerUSSClassName = "distance-traveled-container";
    public static readonly string DistanceTraveledTextUSSClassName = "distance-traveled-text";
    private VisualElementWithClassAndName m_DistanceTraveledContainer = new("DistanceTraveledContainer", new List<string> { "statistics-container", DistanceTraveledContainerUSSClassName });
    private TextElementWithClassAndName m_DistanceTraveledText = new("DistanceTraveledText", new List<string> { DistanceTraveledTextUSSClassName, "statistics-text" });

    public static readonly string ShotsContainerUSSClassName = "shots-container";
    private VisualElementWithClassAndName m_ShotsContainer = new("ShotsContainer", new List<string> { "statistics-container", ShotsContainerUSSClassName });

    public static readonly string ShotsFiredContainerUSSClassName = "shots-fired-container";
    public static readonly string ShotsFiredTextUSSClassName = "shots-fired-text";

    private VisualElementWithClassAndName m_ShotsFiredContainer = new("ShotsFiredContainer", new List<string> { "statistics-container", ShotsFiredContainerUSSClassName });
    private TextElementWithClassAndName m_ShotsFiredText = new("ShotsFiredContainer", new List<string> { ShotsFiredTextUSSClassName, "statistics-text" });

    public static readonly string ShotsHitContainerUSSClassName = "shots-hit-container";
    public static readonly string ShotsHitTextUSSClassName = "shots-hit-text";
    private VisualElementWithClassAndName m_ShotsHitContainer = new("ShotsHitContainer", new List<string> { "statistics-container", ShotsHitContainerUSSClassName });
    private TextElementWithClassAndName m_ShotsHitText = new("", new List<string> { "statistics-text", ShotsHitTextUSSClassName });


    public static readonly string AccuracyContainerUSSClassName = "accuracy-container";
    public static readonly string AccuracyTextUSSClassName = "accuracy-text";
    private VisualElementWithClassAndName m_AccuracyContainer = new("AccuracyContainer", new List<string> { "statistics-container", AccuracyContainerUSSClassName });

    private TextElementWithClassAndName m_AccuracyText = new("AccuracyContainer", new List<string> { AccuracyTextUSSClassName, "statistics-text" });

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

        // Setup layouts
        m_EnemiesKilledContainer.Add(m_EnemiesKilledText);
        m_ShotsFiredContainer.Add(m_ShotsFiredText);
        m_ShotsHitContainer.Add(m_ShotsHitText);
        m_AccuracyContainer.Add(m_AccuracyText);

        m_ShotsContainer.Add(m_ShotsFiredContainer);
        m_ShotsContainer.Add(m_ShotsHitContainer);
        m_ShotsContainer.Add(m_AccuracyContainer);

        Add(m_EnemiesKilledContainer);
        Add(m_ShotsContainer);

        // Setup initial text
        m_EnemiesKilledText.text = 0.ToString();
        m_ShotsFiredText.text = 0.ToString();
        m_ShotsHitText.text = 0.ToString();
        m_AccuracyText.text = 0.ToString();

        generateVisualContent += GenerateVisualContent;


    }

    public void LoadStatistics()
    {
        if (GameStatisticsManager.Instance is null)
        {
            return;
        }

        m_EnemiesKilledText.text = EnemiesKilled.ToString();
        m_ShotsFiredText.text = ShotsFired.ToString();
        m_ShotsHitText.text = ShotsHit.ToString();
        m_AccuracyText.text = ShotsAccuracy.ToString();
    }

    void GenerateVisualContent(MeshGenerationContext context)
    {

    }
}