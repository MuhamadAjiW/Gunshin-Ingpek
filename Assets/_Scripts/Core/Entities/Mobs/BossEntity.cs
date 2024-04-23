using System.Collections.Generic;
using UnityEngine;

public abstract class BossEntity : EnemyEntity, IAccompaniable
{
    // Attributes
    private readonly List<Companion> companionList = new();    
    private readonly List<bool> Activecompanions = new();    

    // Set-Getters
    public List<bool> ActiveCompanions => ActiveCompanions;
    public List<Companion> Companions => companionList;
    public MonoBehaviour CompanionController => this;

    // Functions
    public void ActivateCompanion(int index)
    {
        throw new System.NotImplementedException();
    }

    public void DeactivateCompanion(int index)
    {
        throw new System.NotImplementedException();
    }
}
