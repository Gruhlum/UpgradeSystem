using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    public abstract class StatCollectionDataBase : ScriptableObject
    {
        protected Dictionary<StatType, int> statValues = new Dictionary<StatType, int>();

        public void AddStatValue(Dictionary<StatType, int> dict)
        {
            statValues = dict;
        }

        public abstract List<Stat> GetStats();
    }
}