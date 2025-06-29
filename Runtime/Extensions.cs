using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    public static class Extensions
    {
        public static Stat Find(this List<Stat> stats, StatType type)
        {
            if (stats == null || stats.Count <= 0)
            {
                Debug.Log("huh");
            }
            if (type == null)
            {
                Debug.Log("No type supplied");
            }
            Stat result = stats.Find(x => x.StatType == type);
            return result;
        }

        public static void Add(this Dictionary<StatType, Stat> dict, Stat stat)
        {
            if (stat.StatType == null)
            {
                //Debug.Log(stat.Value);
                return;
            }
            dict.Add(stat.StatType, stat);
        }
    }
}