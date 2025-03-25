using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    public abstract class StatsCollectionData<T> : ScriptableObject where T : StatsCollection
    {
        [SerializeField] private Stat defaultStat = default;

        public T statsCollection;


        [ContextMenu("Copy Default Values")]
        public virtual void AssignDefaultValues()
        {
            if (defaultStat == null)
            {
                return;
            }
            var results = statsCollection.GetStats();
            foreach (var result in results)
            {
                result.CopyFrom(defaultStat);
            }
        }

        public T CreateCopy()
        {
            return statsCollection.CreateCopy() as T;
        }
    }
}