using System;
using UnityEngine;

namespace _Scripts.Core.Game.Data.Statistics
{
    [Serializable]
    public class StatisticsData
    {
        public int enemiesKilled = 0;
        public void Load(string json)
        {
            StatisticsData data = JsonUtility.FromJson<StatisticsData>(json);
            if (data != null)
            {
                enemiesKilled = data.enemiesKilled;
            }
        }

        public string SaveToJson()
        {
            return JsonUtility.ToJson(this);
        }
    }
}