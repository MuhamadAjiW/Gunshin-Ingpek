using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Core.Game.Data.Saves;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingsController : MainMenuScreenController
{
    public NameField nameField;
    public DifficultyDropdown difficultyDropdown;
    public VolumeSlider volumeSlider;

    public void Awake()
    {
        nameField = new();
        difficultyDropdown = new();
        volumeSlider = new();
    }

    public new void OnEnable()
    {
        base.OnEnable();

        MainMenuManager.InitializeBackButton(rootElement);




    }
}
