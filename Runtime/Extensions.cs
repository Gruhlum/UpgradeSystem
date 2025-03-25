using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    public static class Extensions
    {
        public static Stat Find(this List<Stat> stats, StatType type)
        {
            return stats.Find(x =>  x.StatType == type);
        }
    }
}