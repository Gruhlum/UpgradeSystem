using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    public abstract class StatCollectionData<T> : StatCollectionDataBase where T : StatCollection, ICloneable<T>
    {
        public T statCollection;


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
            return statCollection.CreateCopy();
        }
    }
}