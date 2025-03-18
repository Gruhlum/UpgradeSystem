using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public class ClampValue
    {
        public StatClamp clampType;
        //[DrawIf(nameof(clampType), StatClamp.Value)] 
        public int value;
        //[DrawIf(nameof(clampType), StatClamp.Stat)] 
        public StatType statType;

        public ClampValue(StatClamp clampType)
        {
            this.clampType = clampType;
        }
    }
}