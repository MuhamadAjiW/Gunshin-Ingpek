using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


namespace _Scripts.Core.Game.Data.Saves
{
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

        public static string SAVE_PATH = Application.persistentDataPath + "/saves";
        public static int NUMBER_OF_SAVE_SLOT = 3;

        private readonly List<GameSaveData> gameSaves = new();

        public event Action OnGameSavesChange;

        // Constructor
        public void Awake()
        {
            Instance = this;
        }

        public string[] GetAllSavesFileName()
        {
            return Directory.GetFiles(SAVE_PATH);
        }

        public List<GameSaveData> GetSaves()
        {
            return gameSaves;
        }



        public void LoadSaves()
        {
            foreach (string saveFileName in GetAllSavesFileName())
            {
                GameSaveData newSave = new();
                GameLoadResult loadResult = newSave.LoadGame(SAVE_PATH, saveFileName);
                if (loadResult.Equals(GameLoadResult.SUCCESS))
                {
                    gameSaves.Add(newSave);
                    OnGameSavesChange?.Invoke();
                }
            }
        }

        public GameSaveResult NewSave()
        {
            if (gameSaves.Count >= NUMBER_OF_SAVE_SLOT)
            {
                return GameSaveResult.MAX_SAVES_REACHED;
            }
            else
            {
                GameSaveData newSave = new();
                newSave.SaveGame(SAVE_PATH, Guid.NewGuid().ToString());
                gameSaves.Add(newSave);
                return GameSaveResult.SUCCESS;
            }
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




    }
}