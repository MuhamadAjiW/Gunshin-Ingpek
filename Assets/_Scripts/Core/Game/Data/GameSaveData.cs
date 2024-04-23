using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameSaveData : MonoBehaviour 
{
    // Static Instance
    public static GameSaveData instance;

    // Attributes
    public DifficultyType difficulty = DifficultyType.NORMAL;

    // Constructor
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
