using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    public static class Extensions
    {
        public static Stat Find(this List<Stat> stats, StatType type)
        {
            if (stats == null)
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
    }
}