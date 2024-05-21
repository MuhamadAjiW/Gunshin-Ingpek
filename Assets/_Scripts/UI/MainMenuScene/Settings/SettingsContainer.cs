using System;

using Unity.Properties;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

[UxmlElement]
public partial class SettingsContainer : VisualElement
{

    public NameField nameField;
    public DifficultyDropdown difficultyDropdown;
    public VolumeSlider volumeSlider;



    public SettingsContainer()
    {
        name = "SettingsContainer";

        nameField = new()
        {
            name = "NameField"
        };
        difficultyDropdown = new()
        {
            name = "DifficultyDropdown"
        };
        volumeSlider = new()
        {
            name = "VolumeSlider"
        };

        Add(nameField);
        Add(difficultyDropdown);
        Add(volumeSlider);


        generateVisualContent += GenerateVisualContent;
    }

    void GenerateVisualContent(MeshGenerationContext context)
    {
    }

    public void Setup()
    {
        Debug.Assert(GameSettingsManager.Instance);
        Debug.Assert(GameSettingsManager.Instance.gameSettings is not null);
        volumeSlider.value = GameSettingsManager.Instance.gameSettings.SoundVolume;
        nameField.SetName(GameSaveManager.Instance.GetActiveGameSave().playerName);
    }
}