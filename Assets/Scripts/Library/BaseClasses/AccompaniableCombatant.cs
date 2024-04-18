using System.Collections.Generic;

public class AccompaniableCombatant : Combatant, IAccompaniable{
    // Attributes
    private readonly List<Companions> companionList = new();    
    public List<Companions> Companions => companionList;
}
