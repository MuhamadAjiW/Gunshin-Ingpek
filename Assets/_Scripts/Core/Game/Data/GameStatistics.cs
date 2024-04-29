using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Core.Game.Data
{
    [Serializable]
    public class GameStatistics : MonoBehaviour
    {
        // Static Instance
        public static GameStatistics Instance;

        // Saved
        public int enemiesKilled = 0;
        // Helper for Accuracy
        public int shotsFired = 0;

        public int shotsHit = 0;
        // Saved
        public float Accuracy => shotsFired > 0 ? (float)shotsHit / shotsFired * 100 : 0;

        public int DistanceTraveled { get; set; }
        // Helper for PlaytimeFormatted
        public int playtime = 0;
        private int _playTimeHour;
        private int _playTimeMinute;
        private int _playTimeSecond;
        // Saved
        public string PlaytimeFormatted
        {
            get => $"{_playTimeHour:D2}:{_playTimeMinute:D2}:{_playTimeSecond:D2}";
            set
            {
                if (TimeSpan.TryParse(value, out TimeSpan result))
                {
                    playtime = (int)result.TotalSeconds;
                    _playTimeHour = result.Hours + result.Days * 24;
                    _playTimeMinute = result.Minutes;
                    _playTimeSecond = result.Seconds;
                }
            }
        }

        public int SkillsUsed { get; set; }
        public int OrbsCollected { get; set; }
        public int PetsOwned { get; set; }

        // Constructor
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            StartCoroutine(RecordTimeRoutine());
        }

        public void AddEnemiesKilled()
        {
            enemiesKilled++;
        }

        public void AddShotsFired()
        {
            shotsFired++;
        }

        public void AddShotsHit()
        {
            shotsHit++;
        }

        public void AddDistanceTraveled()
        {
            DistanceTraveled++;
        }

        public void AddSkillsUsed()
        {
            SkillsUsed++;
        }

        public void AddOrbsCollected()
        {
            OrbsCollected++;
        }

        public void AddPetsOwned()
        {
            PetsOwned++;
        }

        public void Load(string json)
        {
            GameStatistics data = JsonUtility.FromJson<GameStatistics>(json);
            if (data != null)
            {
                enemiesKilled = data.enemiesKilled;
                shotsFired = data.shotsFired;
                shotsHit = data.shotsHit;
                DistanceTraveled = data.DistanceTraveled;
                PlaytimeFormatted = data.PlaytimeFormatted;
                SkillsUsed = data.SkillsUsed;
                OrbsCollected = data.OrbsCollected;
                PetsOwned = data.PetsOwned;
            }
        }

        public string SaveToJson()
        {
            return JsonUtility.ToJson(this);
        }

        public IEnumerator RecordTimeRoutine()
        {
            TimeSpan ts;
            while (true)
            {
                yield return new WaitForSeconds(1);
                playtime += 1;
                ts = TimeSpan.FromSeconds(playtime);
                _playTimeHour = (int)ts.TotalHours;
                _playTimeMinute = ts.Minutes;
                _playTimeSecond = ts.Seconds;
            }
        }
    }
}