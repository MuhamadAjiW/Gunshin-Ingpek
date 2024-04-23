using System.Collections.Generic;
using UnityEngine;

public interface IAccompaniable
{
    // Set-Getters
    public List<Companion> CompanionList { get; }
    public List<bool> CompanionActive { get; }
    public MonoBehaviour CompanionController { get; }
    public void ActivateCompanion(int index);
    public void DeactivateCompanion(int index);
}
