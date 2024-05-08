using System;
using System.IO;
using UnityEngine;

public class GameSettingsWrapper
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

    public GameSettingsWrapper(string settingsPath)
    {
        if (File.Exists(settingsPath))
        {
            string json = File.ReadAllText(settingsPath);
            GameSettingsWrapper wrapper = JsonUtility.FromJson<GameSettingsWrapper>(json);
            SoundVolume = wrapper.SoundVolume;
        }
    }

    public string SaveToJson()
    {
        return JsonUtility.ToJson(this);
    }

}