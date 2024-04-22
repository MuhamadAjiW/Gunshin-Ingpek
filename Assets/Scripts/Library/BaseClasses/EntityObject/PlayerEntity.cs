using System.Collections.Generic;

public class PlayerEntity : CombatantEntity, IAccompaniable
{
    // Attributes
    protected readonly List<Companions> companionList = new();    
    
    // Set-Getters
    public List<Companions> Companions => companionList;
}
