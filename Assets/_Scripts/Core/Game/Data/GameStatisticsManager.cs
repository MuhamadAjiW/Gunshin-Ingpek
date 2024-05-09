using System;
using System.Collections;
using Unity.Properties;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Core.Game.Data
{
    [Serializable]
    public class GameStatisticsManager : MonoBehaviour
    {
        // Static Instance
<<<<<<< HEAD:Assets/_Scripts/Core/Game/Data/GameStatistics.cs
        public static GameStatistics Instance;
<<<<<<< HEAD

        // Saved
        public int enemiesKilled = 0;
=======
=======
        public static GameStatisticsManager Instance;
>>>>>>> d871ba60 (feat: initial work on statistics element):Assets/_Scripts/Core/Game/Data/GameStatisticsManager.cs

        // Saved
        // [DontCreateProperty]
        public int goonsKilled = 0;
        // [DontCreateProperty]
        public int headgoonsKilled = 0;
        // [DontCreateProperty]
        public int generalsKilled = 0;
        // [DontCreateProperty]
        public int kingsKilled = 0;

        public int EnemiesKilled
        {
            get => goonsKilled + headgoonsKilled + generalsKilled + kingsKilled;
        }

>>>>>>> 434a8b79 (feat: settings working)
        // Helper for Accuracy
        // [DontCreateProperty]
        public int shotsFired = 0;
<<<<<<< HEAD:Assets/_Scripts/Core/Game/Data/GameStatistics.cs
=======
        // [DontCreateProperty]
        public int shotsHit = 0;
>>>>>>> d871ba60 (feat: initial work on statistics element):Assets/_Scripts/Core/Game/Data/GameStatisticsManager.cs

        public int shotsHit = 0;
        // Saved
        public float Accuracy => shotsFired > 0 ? (float)shotsHit / shotsFired * 100 : 0;

        public int DistanceTraveled { get; set; }
        // Helper for PlaytimeFormatted
        [DontCreateProperty]
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
        public void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }

            if (Instance == this)
            {
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            Debug.Log("Loaded Statistics Manager");
        }

        public void Start()
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
            GameStatisticsManager data = JsonUtility.FromJson<GameStatisticsManager>(json);
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