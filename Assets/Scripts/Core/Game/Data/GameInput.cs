using System;
using UnityEngine;

public class GameInput : MonoBehaviour {
    // Static instance
    public static GameInput instance;

    // Attributes
    [NonSerialized] public KeyCode backButton = KeyCode.Escape;
    [NonSerialized] public KeyCode attackButton = KeyCode.Mouse0;
    [NonSerialized] public KeyCode attackAlternateButton = KeyCode.Mouse1;
    [NonSerialized] public KeyCode interactButton = KeyCode.C;

    // Constructor
    protected void Awake(){
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}