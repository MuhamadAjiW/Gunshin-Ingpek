using System.Collections.Generic;

public class PlayerEntity : CombatantEntity, IAccompaniable{
    // Attributes
    private readonly List<Companions> companionList = new();    
    public List<Companions> Companions => companionList;
}
