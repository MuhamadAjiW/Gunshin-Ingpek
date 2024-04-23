using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameSaveData : MonoBehaviour 
{
    // Static Instance
    public static GameSaveData Instance;

    // Attributes
    public DifficultyType difficulty = DifficultyType.NORMAL;
    // Save the currency of the player
    // Save the story state of the player
    // Save the position and the level of the player

    // Constructor
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
