using UnityEngine;

public class GameInput : MonoBehaviour {
    // Static instance
    public static GameInput instance;

    // Attributes
    public KeyCode backButton = KeyCode.Escape;
    public KeyCode attackButton = KeyCode.Z;
    public KeyCode attackAlternateButton = KeyCode.X;
    public KeyCode interactButton = KeyCode.C;

    // Constructor
    protected void Awake(){
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}