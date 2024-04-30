using System;
using System.Collections.Generic;
using UnityEngine;

public class CheatConsole : MonoBehaviour
{
    // GUI Related Attributes
    string textInput = "";
    Vector2 scroll;

    // Cheat Attributes
    private readonly List<string> commandHistory = new();
    private readonly List<string> commandResultHistory = new();
    [SerializeField] int maxCommandHistory = 15;

    private void OnGUI()
    {
        if (GameController.Instance.stateController.GetState() != GameState.CHEAT)
        {
            return;
        }

        float consoleHeight = 30f;
        float padding = 5f;
        float y = 0;

        GUI.Box(new Rect(0, y, Screen.width, consoleHeight), "");
        GUI.Label(new Rect(10, y + 5, 15, consoleHeight), ">");
        GUI.backgroundColor = Color.black;

        GUI.SetNextControlName("CheatTextField");
        textInput = GUI.TextField(new Rect(10 + 15, y + padding, Screen.width - 35, 20), textInput);
        GUI.FocusControl("CheatTextField");

        y += consoleHeight;
        float historyHeight = Screen.height - consoleHeight;

        GUI.Box(new Rect(0, y, Screen.width, historyHeight), "");
        Rect viewport = new(0, 0, Screen.width - 30, 40 * commandHistory.Count);
        scroll = GUI.BeginScrollView(new Rect(0, y + padding, Screen.width, historyHeight - 2 * padding), scroll, viewport);

        for (int i = 0; i < commandHistory.Count; i++)
        {
            int currStartY = (commandHistory.Count - i - 1) * 40;
            GUI.Label(new Rect(2 * padding, currStartY, viewport.width - historyHeight, 20), commandHistory[i]);
            GUI.Label(new Rect(2 * padding, currStartY + 20, viewport.width - historyHeight, 20), commandResultHistory[i]);
        }

        GUI.EndScrollView();

        Event e = Event.current;
        if (e.isKey && e.keyCode == KeyCode.Return && textInput.Trim().Length > 0)
        {
            HandleInput();
        }

        if (e.isKey && e.keyCode == KeyCode.Escape)
        {
            textInput = "";
            GameController.Instance.stateController.HandleEscape();
        }
    }

    private void AddHistory(string command, string result)
    {
        commandHistory.Add(command);
        commandResultHistory.Add(result);
        if (commandHistory.Count > maxCommandHistory)
        {
            commandHistory.RemoveAt(0);
            commandResultHistory.RemoveAt(0);
        }
    }

    private void HandleInput()
    {
        textInput = textInput.Trim();

        if (CheatCommand.cheatCommands.TryGetValue(textInput, out Action command))
        {
            command.Invoke();
            AddHistory("> " + textInput, "Command executed");
        }
        else
        {
            AddHistory("> " + textInput, "Command not found");
        }

        textInput = "";
    }
}