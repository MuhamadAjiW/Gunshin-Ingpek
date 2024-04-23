using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCompanionData", menuName = "Data/Entity/Companion Data")]
public class CompanionData : ScriptableObject
{
    // Note: prefab system is not quite extendable
    // Learn other systems than this obviously
    // but I think the window to learn and implement a new system is not viable within the scope of the project
    public string prefabPath;
    public UnityEngine.GameObject model;
}