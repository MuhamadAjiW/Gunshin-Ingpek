using System;
using System.IO;
using UnityEngine;

public class GameSettingsData
{

    private float m_soundVolume = 100;

    public event Action OnSettingsChanged;

    public float SoundVolume
    {
        get => m_soundVolume;
        set
        {
            m_soundVolume = value;
            OnSettingsChanged?.Invoke();
        }
    }

    public GameSettingsData(string settingsPath)
    {

        if (File.Exists(settingsPath))
        {
            string json = File.ReadAllText(settingsPath);
            Debug.Log($"Loaded settings from {settingsPath}");
            GameSettingsWrapper wrapper = JsonUtility.FromJson<GameSettingsWrapper>(json);
            SoundVolume = wrapper.SoundVolume;
        }

        OnSettingsChanged?.Invoke();
    }

    public string SaveToJson()
    {
        GameSettingsWrapper gameSettingsWrapper = new()
        {
            SoundVolume = this.SoundVolume
        };
        return JsonUtility.ToJson(gameSettingsWrapper);
    }

}