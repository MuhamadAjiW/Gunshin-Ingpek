using System;

[Serializable]
public class PlayerAudioController : AudioController
{
    // Constructor
    public PlayerAudioController(Player player) : base(player.gameObject)
    {
    }
}