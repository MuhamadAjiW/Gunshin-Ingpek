using System;
using UnityEngine;

[Serializable]
public abstract class PetStateController<T> : EntityStateController where T : Companion
{
    // Attributes
    public T pet;

    // Constructor
    public void Init(T pet)
    {
        this.pet = pet;
    }
}