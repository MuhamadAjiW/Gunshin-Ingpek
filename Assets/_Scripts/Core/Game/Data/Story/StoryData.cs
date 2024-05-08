using System;
using System.Collections.Generic;
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

        public bool IsEventComplete(String eventId)
        {
            int idx = events.FindIndex(0, e => e == eventId);
            if(idx > progress.Count - 1)
            {
                Debug.LogError($"Tried checking non event {eventId}. How to resolve: Refer to StoryConfig.cs for valid keys");
                return false;
            }

            return progress[idx];
        }
        
        public void CompleteEvent(String eventId)
        {
            int idx = events.FindIndex(0, e => e == eventId);
            if(idx > progress.Count - 1)
            {
                Debug.LogError($"Tried completing non event {eventId}. How to resolve: Refer to StoryConfig.cs for valid keys");
                return;
            }
            else
            {
                GameController.Instance.InvokeEvent(eventId);
            }

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