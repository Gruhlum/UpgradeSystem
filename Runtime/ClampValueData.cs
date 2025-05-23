using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public class ClampValueData
    {
        public StatClamp clampType = StatClamp.None;
        //[DrawIf(nameof(clampType), StatClamp.Value)] 
        public int value;
        //[DrawIf(nameof(clampType), StatClamp.Stat)] 
        public StatType statType;

        public ClampValueData()
        {
        }
        public ClampValueData(int value)
        {
            this.value = value;
            clampType = StatClamp.Value;
        }
        public ClampValue GenerateClampValue(ClampType type, List<Stat> allStats)
        {
            if (clampType == StatClamp.None)
            {
                return null;
            }
            else if (clampType == StatClamp.Value)
            {
                return new ClampValue(type, value);
            }
            else
            {
                return new ClampValue(type, allStats.Find(statType));
            }
        }

        public ClampValueData CreateCopy()
        {
            ClampValueData clone = new ClampValueData();
            clone.clampType = clampType;
            clone.value = value;
            clone.statType = statType;
            return clone;
        }
    }
}