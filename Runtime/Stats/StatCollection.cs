using System;
using System.Collections;
using System.Collections.Generic;
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

        protected List<Stat> stats;

        public event Action<Stat, Rarity, float> OnStatUpgraded;

        public int GetTotalTickets(Rarity rarity)
        {
            int total = 0;
            foreach (var stat in stats)
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
            Stat stat = stats.Find(type);
            if (stat == null)
            {
                if (stats.Any(x => x.StatType.name == "Experience"))
                {
                    Debug.Log("Is virus");
                }
                else Debug.Log("Is player");

                Debug.Log("Could not find stat of type: " + type);
            }
            return stat;
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
            foreach (var stat in stats)
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
            foreach (var stat in stats)
            {
                stat.OnUpgraded += Stat_OnUpgraded;
            }
        }

        private void Stat_OnUpgraded(Stat stat, Rarity rarity, float efficiency)
        {
            OnStatUpgraded?.Invoke(stat, rarity, efficiency);
        }
        protected List<Stat> GenerateStatList()
        {
            List<Stat> results = new List<Stat>();
            AddStatsToList(results);
            //List<string> debugs = new List<string>();
            //foreach (var result in results)
            //{
            //    if (result != null)
            //    {
            //        debugs.Add(result.StatType.name);
            //    }
            //    else debugs.Add("-");
            //}
            //Debug.Log(debugs.Count + " - " + string.Join(", ", debugs));
            return results;
        }
        public List<Stat> GetStats()
        {
#if UNITY_EDITOR
            if (Application.isEditor)
            {
                return GenerateStatList();
            }
#endif
            return new List<Stat>(stats);
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