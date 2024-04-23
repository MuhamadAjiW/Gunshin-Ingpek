using System;
using UnityEngine;

public abstract class Companion : DamageableEntity 
{
    // Attributes
    public CompanionData data;

    // Getter-Setter
    public IAccompaniable Owner {get; set;}

    // Constructor
    public virtual void Assign(IAccompaniable owner)
    {
        Owner = owner;
    }
}
