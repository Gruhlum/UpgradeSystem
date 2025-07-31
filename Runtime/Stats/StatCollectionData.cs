using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    public abstract class StatCollectionData<T> : StatCollectionDataBase where T : StatCollection, ICloneable<T>
    {
        public T statCollection;

        private void OnValidate()
        {
            if (statCollection != null && string.IsNullOrEmpty(statCollection.Name))
            {
                statCollection.Name = this.name.Replace("Data", string.Empty);
            }
        }

        public void CopyFromStats(T stats)
        {
            this.statCollection = stats.CreateCopy();
        }

        public override List<Stat> GetStats()
        {
            return statCollection.GetStats();
        }
        public T CreateCopy()
        {
            T collection = statCollection.CreateCopy();
            if (statValues != null && statValues.Count > 0)
            {
                Debug.Log(statValues.ElementAt(0));
                collection.ApplyStatValues(statValues);
            }
            return collection;
        }
    }
}