using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerAudioController : AudioController
{
    // Static Attributes
    public const string ATTACK_KEY = "attack_";
    public const string DEATH_KEY = "dead_";
    public const string HIT_KEY = "hit_";
    public const string JUMP_KEY = "jump_";
    public const string SKILL_KEY = "skill_";
    public const string SPRINT_KEY = "sprint_";
    public const string RUNNING_KEY = "running_";
    public const string WALKING_KEY = "walking_";

    // Attributes
    private Player player;
    private readonly string[] keys = new string[]{
        ATTACK_KEY,
        DEATH_KEY,
        HIT_KEY,
        JUMP_KEY,
        SKILL_KEY,
        SPRINT_KEY,
        RUNNING_KEY,
        WALKING_KEY
    };
    private readonly Dictionary<string, int> audioAmount = new();

    // Constructors
    public void Init(Player player)
    {
        base.Init(player);
        foreach (Audio audio in audios)
        {
            foreach(string key in keys)
            {
                if(!audioAmount.ContainsKey(key))
                {
                    audioAmount.Add(key, 0);
                }
                
                if(audio.name.StartsWith(key))
                {
                    audioAmount[key]++;
                }
            }
        }

        this.player = player;
        player.OnDamagedEvent += OnDamaged;
        player.OnDeathEvent += OnDeath;
        player.inputController.OnJumpEvent += OnJump;
        player.stateController.OnStateChangeEvent += OnStateChange;
    }

    public new string Play(string key)
    {
        int index = UnityEngine.Random.Range(1, audioAmount[key] + 1);
        string audioName = key + index;
        base.Play(audioName);

        return audioName;
    }

    public new void Stop(string key)
    {
        for (int i = 1; i < audioAmount[key] + 1; i++)
        {
            string audioName = key + i;
            base.Stop(audioName);
        }
    }

    public void OnDamaged()
    {
        Play(HIT_KEY);
    }

    public void OnDeath()
    {
        Stop(WALKING_KEY);
        Stop(RUNNING_KEY);
        Play(DEATH_KEY);
    }

    public void OnJump()
    {
        Play(JUMP_KEY);
    }

    public void OnStateChange(int oldState, int newState)
    {
        if((newState & PlayerState.SPRINTING) == 0 
        && (oldState & PlayerState.SPRINTING) != 0)
        {
            Stop(RUNNING_KEY);
        }
        else if((newState & PlayerState.SPRINTING) > 0
        && (oldState & PlayerState.SPRINTING) == 0)
        {
            Play(SPRINT_KEY);
            Play(RUNNING_KEY);
        }

        if((newState & PlayerState.WALKING) == 0
        && (oldState & PlayerState.WALKING) != 0)
        {
            Stop(WALKING_KEY);
        }
        else if((newState & PlayerState.WALKING) > 0
        && (oldState & PlayerState.WALKING) == 0)
        {
            Play(WALKING_KEY);
        };
    }
}