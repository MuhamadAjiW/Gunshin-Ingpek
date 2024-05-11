using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingsController : MainMenuScreenController
{

    SettingsContainer settingsContainer;

    public TextField nameField;
    public DropdownField difficultyDropdown;
    public Slider volumeSlider;

    public new void OnEnable()
    {
        base.OnEnable();

        MainMenuManager.InitializeBackButton(rootElement);

        // Setup name field
        nameField = rootElement.Query<TextField>("PlayerNameField");

        nameField.RegisterCallback<ChangeEvent<string>>((evt) =>
        {

            GameSaveManager.Instance.GetActiveGameSave().playerName = evt.newValue;
        });

        // Setup difficulty dropdown

        VisualElement settingsContainer = rootElement.Query<VisualElement>("SettingsContainer");
        difficultyDropdown = settingsContainer.Query<DropdownField>("DifficultyDropdown");
        

        difficultyDropdown.RegisterCallback<ChangeEvent<string>>((evt) =>
        {
            Enum.TryParse(evt.newValue, out DifficultyType newDifficulty);
            GameSaveManager.Instance.GetActiveGameSave().difficulty = newDifficulty;
        });

        List<string> dropdownChoices = Enum.GetNames(typeof(DifficultyType)).ToList();

        difficultyDropdown.index = 1;
        difficultyDropdown.choices = dropdownChoices;
        difficultyDropdown.value = dropdownChoices[1];

        // Setup volume slider
        volumeSlider = rootElement.Query<Slider>("VolumeSlider");
        volumeSlider.lowValue = 0;
        volumeSlider.highValue = 100;
        volumeSlider.value = 100;

        volumeSlider.RegisterCallback<ChangeEvent<float>>((evt) =>
        {
            Debug.Assert(GameSettingsManager.Instance != null);
            Debug.Assert(GameSettingsManager.Instance.gameSettings is not null);
            AudioListener.volume = evt.newValue / volumeSlider.highValue;
            Debug.Log($"Volume in callback: {AudioListener.volume}");
            GameSettingsManager.Instance.gameSettings.SoundVolume = evt.newValue;
        });

        Setup();

    }

    public void Setup()
    {
        Debug.Assert(GameSettingsManager.Instance);
        Debug.Assert(GameSettingsManager.Instance.gameSettings is not null);
        volumeSlider.value = GameSettingsManager.Instance.gameSettings.SoundVolume;
        AudioListener.volume = volumeSlider.value / volumeSlider.highValue;
        Debug.Log($"Volume in setup: {AudioListener.volume}");
        nameField.SetValueWithoutNotify(GameSaveManager.Instance.GetActiveGameSave().playerName);
    }
}
