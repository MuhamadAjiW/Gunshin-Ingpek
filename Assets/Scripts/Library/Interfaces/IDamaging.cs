using System;

public interface IDamaging{
    float Damage{get; set;}
    event Action OnDamage;
}
