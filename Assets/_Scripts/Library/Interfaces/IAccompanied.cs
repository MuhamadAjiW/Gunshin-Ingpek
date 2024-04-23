using System.Collections.Generic;
using UnityEngine;

public interface IAccompaniable
{
    // Set-Getters
    public List<Companion> Companions { get; }
    public List<bool> ActiveCompanions { get; }
    public MonoBehaviour CompanionController { get; }
    public void ActivateCompanion(int index);
    public void DeactivateCompanion(int index);
}
