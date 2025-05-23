using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    public abstract class StatCollectionData<T> : ScriptableObject where T : StatCollection, ICloneable<T>
    {
        //[SerializeField] private Stat defaultStat = default;

        public T statCollection;


        //[ContextMenu("Copy Default Values")]
        //public virtual void AssignDefaultValues()
        //{
        //    if (defaultStat == null)
        //    {
        //        return;
        //    }
        //    var results = statCollection.GetStats();
        //    Debug.Log(results.Count);
        //    foreach (var result in results)
        //    {
        //        result.CopyFrom(defaultStat);
        //    }
        //}

        public T CreateCopy()
        {
            return statCollection.CreateCopy();
        }
    }
}