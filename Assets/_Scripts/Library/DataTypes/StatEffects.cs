using System;
using UnityEngine;


[Serializable]
public class StatEffect
{
    // Attributes
    public string description;
    public int statType;
    public int opType;
    public float value;
    public StatEffectFlag statFlag;

    // Constructor
    public StatEffect(string description, int statType, int opType, float value, StatEffectFlag statFlag = StatEffectFlag.NONE)
    {
        this.description = description;
        this.statType = statType;
        this.opType = opType;
        this.value = value;
        this.statFlag = statFlag;
    }
}