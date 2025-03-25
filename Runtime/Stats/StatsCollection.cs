using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HexTecGames.UpgradeSystem;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    public abstract class StatsCollection : IEnumerable<Stat>
    {
        protected List<Stat> stats;

        public Stat Find(StatType type)
        {
            return stats.Find(type);
        }

        protected abstract StatsCollection InstantiateCopy();
        public StatsCollection CreateCopy()
        {
            StatsCollection clone = InstantiateCopy();
            CopyStats(clone);
            return clone;
        }

        protected abstract void CopyStats(StatsCollection clone);

        protected virtual void InitializeData() { }
        public void Initialize()
        {
            stats = GenerateStatList();
            InitStats();
            InitializeData();
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

        public List<Upgrade> GetValidUpgrades(Rarity rarity)
        {
            List<Upgrade> upgrades = new List<Upgrade>();
            foreach (var stat in stats)
            {
                Upgrade upgrade = stat.GetUpgrade(rarity, stats);
                if (upgrade != null)
                {
                    upgrades.Add(upgrade);
                }
            }
            for (int i = upgrades.Count - 1; i >= 0; i--)
            {
                if (upgrades[i] == null)
                {
                    Debug.Log("huh?");
                    upgrades.RemoveAt(i);
                }
            }
            return upgrades;
        }

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