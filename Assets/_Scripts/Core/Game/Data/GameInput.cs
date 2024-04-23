using System;
using UnityEngine;

public class GameInput : MonoBehaviour 
{
    // Static Instance
    public static GameInput Instance;

    // Attributes
    [NonSerialized] public KeyCode backButton = KeyCode.Escape;
    [NonSerialized] public KeyCode attackButton = KeyCode.Mouse0;
    [NonSerialized] public KeyCode attackAlternateButton = KeyCode.Mouse1;
    [NonSerialized] public KeyCode interactButton = KeyCode.Q;
    [NonSerialized] public KeyCode sprintButton = KeyCode.LeftShift;
    [NonSerialized] public KeyCode inputToggleButton = KeyCode.LeftControl;

    // Constructor
    protected void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
