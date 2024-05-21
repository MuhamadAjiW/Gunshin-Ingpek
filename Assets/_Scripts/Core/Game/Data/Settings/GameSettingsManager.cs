using System;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using UnityEngine;

public class GameSettingsManager : MonoBehaviour
{

    private static string SETTINGS_PATH;

    public GameSettingsData gameSettings;

    public static GameSettingsManager Instance;

    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        Debug.Log("Loaded Settings Manager");
        SETTINGS_PATH = Application.persistentDataPath + "/settings.json";

        LoadSettings();

        gameSettings.OnSettingsChanged += SaveSettings;
    }

    public void SaveSettings()
    {
        File.WriteAllTextAsync(SETTINGS_PATH, gameSettings.SaveToJson());
    }

    public void LoadSettings()
    {
        gameSettings = new(SETTINGS_PATH);
    }

}