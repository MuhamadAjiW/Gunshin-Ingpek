using System;

public interface IDamaging
{
    // Set-Getters
    float Damage{get; set;}
    
    // Events
    event Action OnDamageEvent;
}
