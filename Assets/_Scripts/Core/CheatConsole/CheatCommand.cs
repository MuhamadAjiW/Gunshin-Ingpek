using System;
using System.Collections.Generic;

public class CheatCommand
{
    public static readonly Dictionary<string, Action> cheatCommands = new(){
        { "no_damage", () => { throw new NotImplementedException(); }},
        { "1_hit_kill", () => { throw new NotImplementedException(); }},
        { "motherlode", () => { throw new NotImplementedException(); }},
        { "x2_speed", () => { GameController.Instance.player.BaseSpeed *= 2; }},
        { "full_hp_pet", () => { throw new NotImplementedException(); }},
        { "kill_pet", () => { throw new NotImplementedException(); }},
        { "orb", () => { throw new NotImplementedException(); }},
        { "skip", () => { throw new NotImplementedException(); }},
    };
}
