using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameSaveData : MonoBehaviour {
    // Attributes
    public static GameSaveData instance;

    public DifficultyType difficulty = DifficultyType.NORMAL;

    // Constructor
    private void Awake(){
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
