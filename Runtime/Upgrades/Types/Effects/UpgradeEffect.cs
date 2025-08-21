using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics.UI;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{    
    [System.Serializable]
    public abstract class UpgradeEffect
    {
        public Rarity rarity;
        public int tickets = 100;

        public abstract void Apply(Rarity rarity, float efficiency);
        public abstract bool IsValidUpgrade(Stat stat, Rarity rarity, float efficiency);
        public abstract StatUpgrade GetUpgrade(Stat stat, Rarity rarity, Efficiency singleEfficiency);
        public abstract bool CanBeMultiUpgrade(Rarity rarity, float efficiency);
        public abstract bool CanBeOverTimeUpgrade(Rarity rarity, float efficiency);
        public abstract TableText GetMainDescription(Rarity rarity, Efficiency efficiency);
        public abstract string GetBonusDescription(Rarity rarity, Efficiency efficiency);
    }
}