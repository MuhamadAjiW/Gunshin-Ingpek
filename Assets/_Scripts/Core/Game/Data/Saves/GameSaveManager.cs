using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using _Scripts.Core.Game.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public List<GameSaveData> gameSaves;

    private GameSaveData activeGameSave;
    public int activeGameSaveIndex = -1;


    public event Action OnGameSavesChange;

    // Constructor
    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        if (Instance == this)
        {
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

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
            Debug.Log("Statistics file loaded");
        }
        else
        {

            Debug.LogWarning("Statistics file not found. Making new statistics file");
            PersistStatistics();
        }
    }

    public void PersistStatistics()
    {
        File.WriteAllTextAsync(STATS_FILE_PATH, GameStatisticsManager.Instance.SaveToJson());
    }

    public GameSaveResult NewSave()
    {
        if (gameSaves.Count >= NUMBER_OF_SAVE_SLOT)
        {
            return GameSaveResult.MAX_SAVES_REACHED;
        }
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
=======
        
=======

>>>>>>> 56798ab4 (feat: add guard in awake function of singletons to keep singleton single)
=======

>>>>>>> 5bc01392 (feat: added delete all saves)
        activeGameSaveIndex = gameSaves.Count;
>>>>>>> d93e8bd0 (fix: save, load)
        activeGameSave.SaveGame(SAVE_PATH);
        gameSaves.Add(activeGameSave);

        Debug.LogWarning($"Save Index: {activeGameSaveIndex}");
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
        ScenesManager.Instance.LoadNewGame();
        SceneManager.sceneLoaded += LoadGame;
    }

    public void OverrideSave()
    {
        gameSaves.ElementAt(0).Delete(SAVE_PATH);
        gameSaves.RemoveAt(0);
        gameSaves.Add(activeGameSave);
        activeGameSave.SaveGame(SAVE_PATH);
        activeGameSaveIndex = 0;
    }

    public void DeleteAllSaves()
    {
        gameSaves.ForEach((save) =>
        {
            save.Delete(SAVE_PATH);
        });

        gameSaves = new();
    }

    public void SaveGame()
    {
        // Saving side effects
        GameController.Instance.player.Health = GameController.Instance.player.MaxHealth;
        GameController.Instance.StartCutscene(StoryConfig.KEY_CUTSCENE_SAVE);
        Instance.activeGameSave.positionData.point = GameController.Instance.player.transform.position;
        Instance.activeGameSave.weaponPoolIndex =
            new List<int>(GameController.Instance.player.weaponList.Where(e => e.poolIndex != 0).Select(
            (e) => e.poolIndex));

        PersistActiveSave();
    }

    private void PersistActiveSave()
    {
        gameSaves.ElementAt(activeGameSaveIndex).SaveGame(SAVE_PATH);
    }

    private void LoadGame(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= LoadGame;

        GameController.Instance.player.transform.position = Instance.gameSaves[activeGameSaveIndex].positionData.point;
        
        if(Instance.gameSaves[activeGameSaveIndex].petData != null)
        {
            GameController.Instance.player.companionList = new List<Companion>(Instance.gameSaves[activeGameSaveIndex].petData.Select(Companion.NewCompanionByType));
        }

        Debug.Assert(GameController.Instance.player.weaponList != null);
        Debug.Assert(Instance.gameSaves[activeGameSaveIndex].weaponPoolIndex != null);
        Instance.gameSaves[activeGameSaveIndex].weaponPoolIndex.ForEach(e => GameController.Instance.player.weaponList.Add(EventManager.Instance.WeaponPool[e]));
    }

    public void DeleteActiveGameSave()
    {
        DeleteSave(activeGameSaveIndex);
    }

}
