using System;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

namespace _Scripts.Core.Game.Data.Story
{
    [Serializable]
    public class StoryData: DataClass
    {
        public List<String> events = new();
        public List<Boolean> progress = new();

        public StoryData(List<String> eventsList)
        {
            foreach (string eventId in eventsList)
            {
                events.Add(eventId);
                progress.Add(false);
            }
        }
        
        public void CompleteEvent(String eventId)
        {
            int idx = events.FindIndex(0, e => e == eventId);
            progress[idx] = true;
        }

        public override void Load(string json)
        {
            StoryData data = JsonUtility.FromJson<StoryData>(json);
            if (data != null)
            {
                events = data.events;
                progress = data.progress;
            }
            
        }
        
        public override string SaveToJson()
        {
            return JsonUtility.ToJson(this);
        }
        
    }
}