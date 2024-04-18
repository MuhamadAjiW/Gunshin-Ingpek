using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameData : MonoBehaviour {
    // Attributes
    public static GameData instance;

    public static DifficultyType difficulty = DifficultyType.NORMAL;
    
    // Constructor
    private void Awake(){
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Functions
    public void Save(PlayerStats playerStats){
    
    }
}