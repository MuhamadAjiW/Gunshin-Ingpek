using System;
using UnityEngine;

public class GameInput : MonoBehaviour {
    // Static instance
    public static GameInput instance;

    // Attributes
    [NonSerialized] public KeyCode backButton = KeyCode.Escape;
    [NonSerialized] public KeyCode attackButton = KeyCode.Z;
    [NonSerialized] public KeyCode attackAlternateButton = KeyCode.X;
    [NonSerialized] public KeyCode interactButton = KeyCode.C;

    // Constructor
    protected void Awake(){
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}