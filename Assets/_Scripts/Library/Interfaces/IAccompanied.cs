using System.Collections.Generic;
using UnityEngine;

public interface IAccompaniable
{
    // Set-Getters
    public List<Companion> Companions { get; }
    public MonoBehaviour CompanionController { get; }
}
