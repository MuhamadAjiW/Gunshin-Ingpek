using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameSettingsManager : MonoBehaviour
{

    private static string SETTINGS_PATH;

    public GameSettingsWrapper gameSettings;

    public static GameSettingsManager Instance;

    public void Awake()
    {
        Instance = this;
        SETTINGS_PATH = Application.persistentDataPath + "/settings";
        gameSettings = new(SETTINGS_PATH);

        gameSettings.OnSettingsChanged += SaveSettings;
    }

    public void SaveSettings()
    {
        File.WriteAllTextAsync(SETTINGS_PATH, gameSettings.SaveToJson());
    }

}