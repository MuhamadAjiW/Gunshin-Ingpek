using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : CombatantEntity, IAccompaniable
{
    // Attributes
    public List<Companion> companionList = new();    
    
    // Set-Getters
    public List<Companion> Companions => companionList;
    public MonoBehaviour CompanionController => this;
}
