using UnityEngine;

public class GameControls : MonoBehaviour {
    // Attributes
    public static GameControls instance;
    public KeyCode backButton = KeyCode.Escape;
    public KeyCode attackButton = KeyCode.Z;

    // Constructor
    private void Awake(){
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}