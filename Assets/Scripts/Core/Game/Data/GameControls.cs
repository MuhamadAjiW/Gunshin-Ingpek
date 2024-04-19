using UnityEngine;

public class GameControls : MonoBehaviour {
    // Static instance
    public static GameControls instance;

    // Attributes
    public KeyCode backButton = KeyCode.Escape;
    public KeyCode attackButton = KeyCode.Z;
    public KeyCode interactButton = KeyCode.X;

    // Constructor
    protected void Awake(){
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}