using UnityEngine;

public class Goon : EnemyEntity
{
    // Static Attributes
    public const string ObjectIdPrefix = "Goon";

    // Attributes

    // Constructor
    new protected void Start()
    {
        base.Start();
        SetIdPrefix(ObjectIdPrefix);
    }
}