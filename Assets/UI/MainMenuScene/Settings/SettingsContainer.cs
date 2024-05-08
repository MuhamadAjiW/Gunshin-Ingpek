using System;
using _Scripts.Core.Game.Data.Saves;
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

        nameField = new();
        difficultyDropdown = new();
        volumeSlider = new();

        Add(nameField);
        Add(difficultyDropdown);
        Add(volumeSlider);

        volumeSlider.RegisterCallback<ChangeEvent<float>>((evt) =>
        {
            Debug.Assert(GameSettingsManager.Instance is not null);
            Debug.Assert(GameSettingsManager.Instance.gameSettings is not null);
            GameSettingsManager.Instance.gameSettings.SoundVolume = evt.newValue;
        });

        nameField.RegisterCallback<ChangeEvent<string>>((evt) =>
        {

            GameSaveManager.Instance.GetActiveGameSave().playerName = evt.newValue;
        });

        difficultyDropdown.RegisterCallback<ChangeEvent<string>>((evt) =>
        {
            Enum.TryParse(evt.newValue, out DifficultyType newDifficulty);
            GameSaveManager.Instance.GetActiveGameSave().difficulty = newDifficulty;
        });


        generateVisualContent += GenerateVisualContent;
    }

    void GenerateVisualContent(MeshGenerationContext context)
    {
    }
}