using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : CombatantEntity, IAccompaniable
{
    // Attributes
    public List<Companion> companionList = new();    
    public List<bool> activeCompanions = new();    
    
    // Set-Getters
    public List<Companion> Companions => companionList;
    public List<bool> ActiveCompanions => activeCompanions;
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
