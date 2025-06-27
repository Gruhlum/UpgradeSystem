using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HexTecGames.Basics;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    public abstract class StatCollection : IEnumerable<Stat>
    {
        public CollectionType CollectionType
        {
            get
            {
                return collectionType;
            }
            protected set
            {
                collectionType = value;
            }
        }
        [SerializeField] private CollectionType collectionType;

        public List<Stat> Stats
        {
            get
            {
                return stats.Values.ToList();
            }
        }
        protected Dictionary<StatType, Stat> stats;

        public event Action<Stat, int> OnStatUpgraded;

        public int GetTotalTickets(Rarity rarity)
        {
            int total = 0;
            foreach (var stat in stats)
            {
                if (stat.Value.IsValidUpgrade(rarity))
                {
                    total += stat.Value.UpgradeInfo.Tickets;
                }
            }
            return total;
        }

        public Stat Find(StatType type)
        {
            if (stats.TryGetValue(type, out Stat stat))
            {
                return stat;
            }
            Debug.Log("Could not find stat of type: " + type);
            return null;
        }

        protected virtual void InitializeData() { }
        public void Initialize()
        {
            stats = GenerateStatList();
            InitStats();
            InitializeData();
            AddEvents();
        }
        private void InitStats()
        {
            foreach (var stat in stats.Values)
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
            foreach (var stat in stats.Values)
            {
                stat.OnUpgraded += Stat_OnUpgraded;
            }
        }

        public void LevelUp()
        {
            foreach (var stat in stats)
            {
                stat.Value.LevelUp();
            }
        }
        private void Stat_OnUpgraded(Stat stat, int increase)
        {
            OnStatUpgraded?.Invoke(stat, increase);
        }
        protected Dictionary<StatType, Stat> GenerateStatList()
        {
            Dictionary<StatType, Stat> results = new Dictionary<StatType, Stat>();
            AddStatsToList(results);
            return results;
        }
        public Dictionary<StatType, Stat> GetStats()
        {
#if UNITY_EDITOR
            if (Application.isEditor)
            {
                return GenerateStatList();
            }
#endif
            return stats;
        }
        protected abstract void AddStatsToList(Dictionary<StatType, Stat> stats);

        public IEnumerator<Stat> GetEnumerator()
        {
            return stats.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}