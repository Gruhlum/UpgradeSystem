using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public class ClampValueData
    {
        public StatClamp clampType;
        //[DrawIf(nameof(clampType), StatClamp.Value)] 
        public int value;
        //[DrawIf(nameof(clampType), StatClamp.Stat)] 
        public StatType statType;

        public ClampValueData(StatClamp clampType)
        {
            this.clampType = clampType;
        }
        public ClampValueData(StatClamp clampType, int value) : this(clampType)
        {
            this.value = value;
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
            ClampValueData clone = new ClampValueData(clampType);
            clone.value = value;
            clone.statType = statType;
            return clone;
        }
    }
}