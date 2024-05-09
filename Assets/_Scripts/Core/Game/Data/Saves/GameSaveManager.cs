using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using _Scripts.Core.Game.Data;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{
    public enum GameSaveResult
    {
        MAX_SAVES_REACHED,
        SUCCESS,
    };

    public enum GameLoadResult
    {
        NO_FILE_FOUND,
        SUCCESS,
    };

    // Static Instance
    public static GameSaveManager Instance;

    private static string SAVE_PATH;

    private static string STATS_FILE_PATH;

    private static int NUMBER_OF_SAVE_SLOT;

    private List<GameSaveData> gameSaves;

    private GameSaveData activeGameSave;
    private int activeGameSaveIndex = -1;


    public event Action OnGameSavesChange;

    // Constructor
    public void Awake()
    {
        Instance = this;
        Debug.Log("Loaded Saves Manager");
        Debug.Assert(Instance is not null);
        activeGameSave = new();
        SAVE_PATH = Application.persistentDataPath + "/saves";
        Directory.CreateDirectory(SAVE_PATH);
        STATS_FILE_PATH = Application.persistentDataPath + "/stats.json";
        NUMBER_OF_SAVE_SLOT = 3;
        gameSaves = new();
        FetchSaves();
    }

    public string[] GetAllSavesFileName()
    {
        return Directory.GetFiles(SAVE_PATH);
    }

    public List<GameSaveData> GetSaves()
    {
        return gameSaves;
    }

    public void FetchSaves()
    {
        foreach (string saveFileName in GetAllSavesFileName())
        {
            GameSaveData newSave = new();
            GameLoadResult loadResult = newSave.LoadGame(saveFileName);
            if (loadResult.Equals(GameLoadResult.SUCCESS))
            {
                gameSaves.Add(newSave);
                OnGameSavesChange?.Invoke();
            }
        }

        gameSaves = gameSaves.OrderBy(gameSave => gameSave.writeTime).ToList();
    }

    public void LoadStatistics()
    {
        if (File.Exists(STATS_FILE_PATH))
        {
            GameStatisticsManager.Instance.Load(File.ReadAllText(STATS_FILE_PATH));
        }
        else
        {
            Debug.LogError("Statistics file not found");
        }
    }

    public void SaveStatistics()
    {
        File.WriteAllTextAsync(STATS_FILE_PATH, GameStatisticsManager.Instance.SaveToJson());
    }

    public GameSaveResult NewSave()
    {
        if (gameSaves.Count >= NUMBER_OF_SAVE_SLOT)
        {
            return GameSaveResult.MAX_SAVES_REACHED;
        }
        activeGameSave.SaveGame(SAVE_PATH);
        gameSaves.Add(activeGameSave);
        return GameSaveResult.SUCCESS;
    }

    public int GetNumberOfSaves()
    {
        return gameSaves.Count;
    }

    public void DeleteSave(int index)
    {
        if (index > GetNumberOfSaves() - 1)
        {
            Debug.LogError("Save file does not exist");
            return;
        }

        gameSaves.RemoveAt(index);
        OnGameSavesChange?.Invoke();
    }

    public GameSaveData GetActiveGameSave()
    {
        return activeGameSave;
    }

    public void ResetActiveGameSave()
    {
        activeGameSave = new();
    }

    private void SetActiveGameSaveFromGameSaves(int index)
    {
        GameSaveData gameSaveToLoad = gameSaves.ElementAt(index);
        activeGameSave = gameSaveToLoad;
    }

    public void LoadExistingSave(int index)
    {
        activeGameSaveIndex = index;
        SetActiveGameSaveFromGameSaves(index);
        // Do loading shit
        GameController.Instance.player.transform.position = Instance.gameSaves[activeGameSaveIndex].positionData.point;
        
    }

    public void OverrideSave()
    {
        gameSaves.RemoveAt(0);
        gameSaves.Add(activeGameSave);
        activeGameSaveIndex = 0;
    }

    public void PersistActiveSave()
    {
        gameSaves.ElementAt(activeGameSaveIndex).SaveGame(SAVE_PATH);
    }

}
