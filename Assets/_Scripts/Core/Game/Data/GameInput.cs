using System;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    // Static Instance
    public static GameInput Instance;

    // Attributes
<<<<<<< HEAD
    [NonSerialized] public KeyCode backButton = KeyCode.Escape;
    [NonSerialized] public KeyCode attackButton = KeyCode.Mouse0;
    [NonSerialized] public KeyCode attackAlternateButton = KeyCode.Mouse1;
    [NonSerialized] public KeyCode attackSkillButton = KeyCode.Mouse2;
    [NonSerialized] public KeyCode interactButton = KeyCode.Q;
    [NonSerialized] public KeyCode sprintButton = KeyCode.LeftShift;
    [NonSerialized] public KeyCode inputToggleButton = KeyCode.LeftControl;
    [NonSerialized] public KeyCode aimToggleButton = KeyCode.E;
    [NonSerialized]
    public List<KeyCode> cheatTriggerButton = new()
    {
        KeyCode.BackQuote,
        KeyCode.C
    };
=======
    [HideInInspector] public KeyCode backButton = KeyCode.Escape;
    [HideInInspector] public KeyCode attackButton = KeyCode.Mouse0;
    [HideInInspector] public KeyCode attackAlternateButton = KeyCode.Mouse1;
    [HideInInspector] public KeyCode attackSkillButton = KeyCode.F;
    [HideInInspector] public KeyCode interactButton = KeyCode.Q;
    [HideInInspector] public KeyCode sprintButton = KeyCode.LeftShift;
    [HideInInspector] public KeyCode inputToggleButton = KeyCode.LeftControl;
    [HideInInspector] public KeyCode aimToggleButton = KeyCode.E;
>>>>>>> 83a1ae9e (feat: AI goon attack in range)

    // Constructor
    protected void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
