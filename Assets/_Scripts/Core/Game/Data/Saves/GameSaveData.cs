using System;
using System.Collections.Generic;
using System.IO;
using _Scripts.Core.Game.Data.Currency;
using _Scripts.Core.Game.Data.Position;
using _Scripts.Core.Game.Data.Story;
using UnityEngine;

namespace _Scripts.Core.Game.Data.Saves
{
    public class GameSaveData
    {
        // Attributes

        public DateTime writeTime;

        public string playerName = "";
        public DifficultyType difficulty = DifficultyType.NORMAL;
        public List<string> events = new();
        // Save the currency of the player
        public CurrencyData currencyData = new();
        // Save the position and the level of the player
        public PositionData positionData = new();
        // Save the story state of the player
        public StoryData storyData;

        // Constructor
        public GameSaveData()
        {
            storyData = new StoryData(events);
        }

        public void SaveGame(string path, string name)
        {
            GameDataWrapper wrapper = new()
            {
                difficulty = this.difficulty,
                events = this.events,
                currencyData = this.currencyData,
                positionData = this.positionData,
                storyData = this.storyData,
                playerName = this.playerName,
            };

            string json = JsonUtility.ToJson(wrapper, true);
            string filePath = string.Format("{0}/{1}.json", path, name);
            File.WriteAllText(filePath, json);
            Debug.Log("Game saved to " + filePath);
        }

        public GameSaveManager.GameLoadResult LoadGame(string path, string name)
        {
            string filePath = string.Format("{0}/{1}", path, name);
            if (File.Exists(filePath))
            {
                writeTime = File.GetLastWriteTime(filePath);
                string json = File.ReadAllText(filePath);
                GameDataWrapper wrapper = JsonUtility.FromJson<GameDataWrapper>(json);

                this.difficulty = wrapper.difficulty;
                this.events = wrapper.events;
                this.currencyData = wrapper.currencyData;
                this.positionData = wrapper.positionData;
                this.storyData = wrapper.storyData;
                this.playerName = wrapper.playerName;
                Debug.Log("Game loaded from " + filePath);
                return GameSaveManager.GameLoadResult.SUCCESS;
            }
            else
            {
                Debug.LogError("Save file not found in " + path);
                return GameSaveManager.GameLoadResult.NO_FILE_FOUND;
            }
        }


    }
}