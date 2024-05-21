using System;
using System.Collections.Generic;

using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class GameSavesContainer : VisualElement
{
    public static readonly string GameSavesContainerUSSClassName = "game-saves-container";

    public static readonly string NoSavesTextUSSClassName = "no-saves-text";
    public static readonly List<string> GameSaveCardUSSClassNameList = new() { "game-save-card", "has-text", "rounded" };

    private TextElementWithClassAndName m_NoSavesText = new("NoSavesText", new List<string> { NoSavesTextUSSClassName, "info-prompt-text" })
    {
        text = "No Save Yet!"
    };


    public List<GameSaveData> Saves
    {
        get => GameSaveManager.Instance.GetSaves();

    }

    public GameSavesContainer()
    {

        name = "GameSavesContainer";
        AddToClassList(GameSavesContainerUSSClassName);

        Add(m_NoSavesText);
        Add(new GameSaveCard(new GameSaveData()
        {
            writeTime = DateTime.Now
        }, 0, GameSaveCardUSSClassNameList));

        generateVisualContent += GenerateVisualContent;

    }

    public void LoadSave()
    {
        Clear();
        if (GameSaveManager.Instance is not null)
        {
            Debug.Log($"Save: {Saves.Count}");

            if (Saves.Count <= 0)
            {
                Debug.Log("No saves!");
                Add(m_NoSavesText);
            }
            else
            {
                int i = 0;
                foreach (GameSaveData save in Saves)
                {
                    // Add GameSave for each save data
                    GameSaveCard gameSaveCard = new(save, i, GameSaveCardUSSClassNameList);
                    Add(gameSaveCard);
                    i++;
                }
            }
        }

    }

    void GenerateVisualContent(MeshGenerationContext context)
    {

    }
}