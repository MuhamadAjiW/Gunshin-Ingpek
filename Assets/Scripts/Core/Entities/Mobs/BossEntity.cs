using System.Collections.Generic;

public abstract class BossEntity : EnemyEntity, IAccompaniable{
    // Attributes
    private readonly List<Companions> companionList = new();    

    // Set-Getters
    public List<Companions> Companions => companionList;
}
