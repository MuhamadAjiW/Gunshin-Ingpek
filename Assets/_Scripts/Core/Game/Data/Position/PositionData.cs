using System;
using UnityEngine;

namespace _Scripts.Core.Game.Data.Position
{
    [Serializable]
    public class PositionData
    {
        public Vector3? point = null;

        public void Load(string json)
        {
            PositionData data = JsonUtility.FromJson<PositionData>(json);
            point = data.point;
        }

        public string SaveToJson()
        {
            return JsonUtility.ToJson(this);
        }
    }
}