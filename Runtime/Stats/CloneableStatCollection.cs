using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public abstract class CloneableStatCollection<T> : StatCollection, ICloneable<T> where T : StatCollection
    {
        protected abstract T InstantiateCopy();

        public T CreateCopy()
        {
            T clone = InstantiateCopy();
            CopyStats(clone);
            return clone;
        }

        protected virtual void CopyStats(T target)
        {
            target.Name = Name;
            stats = GenerateStatList();

            List<Stat> originalStats = GetStats();
            List<Stat> targetStats = target.GetStats();

            if (originalStats.Count != targetStats.Count)
            {
                Debug.Log("Not equal number of stats: " + Name + ": " + originalStats.Count + " - " + targetStats.Count);
            }

            for (int i = 0; i < originalStats.Count; i++)
            {
                targetStats[i].CopyFrom(originalStats[i]);
            }
        }
    }
}