using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    public abstract class StatCollection : IEnumerable<Stat>
    {
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        [SerializeField] private string name;

        protected List<Stat> stats;

        public event Action<Stat, int> OnStatUpgraded;

        //private Dictionary<StatType, Stat> statDictionary;

        private bool isDataInitialized;

        public StatCollection()
        {
            stats = GenerateStatList();
        }

        public int GetTotalTickets(Rarity rarity)
        {
            int total = 0;
            foreach (Stat stat in stats)
            {
                if (stat.IsValidUpgrade(rarity))
                {
                    total += stat.UpgradeInfo.Tickets;
                }
            }
            return total;
        }

        public Stat Find(StatType type)
        {
            return stats.Find(type);

            //if (statDictionary.TryGetValue(type, out Stat stat))
            //{
            //    return stat;
            //}
            //Debug.Log("Could not find stat of type: " + type);
            //return null;
        }

        protected virtual void InitializeData() { }
        public void Initialize()
        {
            stats = GenerateStatList();
            //GenerateDictionary();
            InitStats();
            if (isDataInitialized)
            {
                return;
            }
            isDataInitialized = true;

            InitializeData();
            AddEvents();
        }

        //private void GenerateDictionary()
        //{
        //    statDictionary = new Dictionary<StatType, Stat>();

        //    foreach (var stat in stats)
        //    {
        //        if (stat == null)
        //        {
        //            Debug.LogError("Stat is null!");
        //        }
        //        else if (stat.StatType == null)
        //        {
        //            Debug.LogError("StatType is null!");
        //        }
        //        statDictionary.Add(stat);
        //    }
        //}


        private void InitStats()
        {
            foreach (Stat stat in stats)
            {
                if (stat == null)
                {
                    Debug.Log("huh");
                }
                stat.Initialize(stats);
            }
        }
        private void AddEvents()
        {
            foreach (Stat stat in stats)
            {
                stat.OnUpgraded += Stat_OnUpgraded;
            }
        }

        public void ApplyStatValues(Dictionary<StatType, int> statValues)
        {
            foreach (KeyValuePair<StatType, int> statValue in statValues)
            {
                if (stats == null || stats.Count <= 0)
                {
                    Debug.Log("HUUUH");
                    return;
                }
                if (statValue.Key == null)
                {
                    Debug.Log("HUsddsUUH");
                    return;
                }

                Stat stat = stats.Find(statValue.Key);
                if (stat == null)
                {
                    Debug.Log("Could not find stat: " + statValue.Key);
                }

                stat.FlatValue += statValue.Value;
            }
        }

        public void LevelUp()
        {
            foreach (Stat stat in stats)
            {
                stat.LevelUp();
            }
        }
        private void Stat_OnUpgraded(Stat stat, int increase)
        {
            OnStatUpgraded?.Invoke(stat, increase);
        }
        protected List<Stat> GenerateStatList()
        {
            List<Stat> results = new List<Stat>();
            AddStatsToList(results);

            return results;
        }
        public List<Stat> GetStats()
        {
            return stats;
        }
        protected abstract void AddStatsToList(List<Stat> stats);

        public IEnumerator<Stat> GetEnumerator()
        {
            return stats.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}