using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using _Scripts.Core.Game.Data.Currency;
using _Scripts.Core.Game.Data.Position;
using _Scripts.Core.Game.Data.Story;
using UnityEditor;
using UnityEngine;

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
    public List<Companion.Type> petData = new();
    public List<int> weaponPoolIndex = new();

    public string id;

    // Constructor
    public GameSaveData()
    {
        writeTime = DateTime.Now;
        id = Guid.NewGuid().ToString();
        storyData = new StoryData(StoryConfig.EVENTS);
    }

    public void SaveGame(string path)
    {
        Debug.Log("Saving game 0...");
        GameDataWrapper wrapper = new()
        {
            difficulty = this.difficulty,
            events = this.events,
            currencyData = this.currencyData,
            positionData = this.positionData,
            storyData = this.storyData,
            playerName = this.playerName,
            petData = this.petData,
            weaponPoolIndex = this.weaponPoolIndex,
            id = this.id
        };

        string json = JsonUtility.ToJson(wrapper, true);
        Debug.Log("Saving game 1...");

        string filePath = string.Format($"{path}/{id}.json");
        Debug.Log("Saving game 2...");

        File.WriteAllText(filePath, json);

        Debug.Log("Saving game 3...");
        Debug.Log("Game saved to " + filePath);
    }

    public GameSaveManager.GameLoadResult LoadGame(string path)
    {
        Debug.Log(path);
        if (File.Exists(path))
        {
            writeTime = File.GetLastWriteTime(path);
            string json = File.ReadAllText(path);
            GameDataWrapper wrapper = JsonUtility.FromJson<GameDataWrapper>(json);
            this.difficulty = wrapper.difficulty;
            this.events = wrapper.events;
            this.currencyData = wrapper.currencyData;
            this.positionData = wrapper.positionData;
            this.storyData = wrapper.storyData;
            this.playerName = wrapper.playerName;
            this.petData = wrapper.petData;
            this.weaponPoolIndex = wrapper.weaponPoolIndex;
            this.id = wrapper.id;
            Debug.Log("Game loaded from " + path);
            return GameSaveManager.GameLoadResult.SUCCESS;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return GameSaveManager.GameLoadResult.NO_FILE_FOUND;
        }
    }

    public void Delete(string path)
    {
        string filePath = $"{path}/{id}.json";
        Debug.Log($"Deleting {filePath}");

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            return;
        }

        Debug.LogError("The file to be deleted not available!");


    }


}