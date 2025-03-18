using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public class StatData
    {
        public StatType type;
        public int startValue;
        public int increase;

        public ClampValue minValue;
        public ClampValue maxValue;
        public string formatting;

        //Create every instance
        //Link any Stats

        public Stat GenerateStat()
        {
            if (minValue.clampType != StatClamp.None || maxValue.clampType != StatClamp.None)
            {
                return new ClampedStat(type);
            }
            else return new Stat(type);
        }

        public void ApplyData(Stat stat, List<Stat> allStats)
        {
            stat.ApplyData(allStats, this);
        }
    }
}