using System.Collections.Generic;

public abstract class AccompaniableEnemyObject : EnemyEntity, IAccompaniable{
    // Attributes
    private readonly List<Companions> companionList = new();    
    public List<Companions> Companions => companionList;
}
