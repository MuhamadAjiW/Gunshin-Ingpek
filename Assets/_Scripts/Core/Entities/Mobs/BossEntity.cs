using System.Collections.Generic;
using UnityEngine;

public abstract class BossEntity : EnemyEntity, IAccompaniable
{
    // Attributes
    private readonly List<Companion> companionList = new();    

    // Set-Getters
    public List<Companion> Companions => companionList;
    public MonoBehaviour CompanionController => this;
}
