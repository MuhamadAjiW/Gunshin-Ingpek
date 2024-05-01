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

    // Constructor
    public StatEffect(string description, int statType, int opType, float value)
    {
        this.description = description;
        this.statType = statType;
        this.opType = opType;
        this.value = value;
    }
}