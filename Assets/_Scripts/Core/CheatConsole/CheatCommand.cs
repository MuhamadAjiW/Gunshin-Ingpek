using System;

public class CheatCommand
{
    public string commandId;
    public Action command;

    public CheatCommand(string commandId, Action command)
    {
        this.commandId = commandId;
        this.command = command;
    }

    public void Invoke()
    {
        command.Invoke();
    }
}