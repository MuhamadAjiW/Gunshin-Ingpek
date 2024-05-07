using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCutsceneData", menuName = "Data/Cutscene/Cutscene Data")]
public class CutsceneData : ScriptableObject
{
    public List<DialogData> dialogs = new();
}