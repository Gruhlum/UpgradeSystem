using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics;
using HexTecGames.Basics.UI;
using HexTecGames.UpgradeSystem;
using UnityEngine;

namespace HexTecGames
{
    [System.Serializable]
    public abstract class UpgradeItem
    {
        public Rarity rarity;
        public StatType statType;
        public int tickets;

        public abstract void Apply(Rarity rarity, float efficiency);
        public abstract bool IsValidUpgrade(Stat stat, Rarity rarity, float efficiency);
        public abstract StatUpgrade GetUpgrade(Stat stat, Rarity rarity, Efficiency singleEfficiency);
        public abstract bool CanBeMultiUpgrade(Rarity rarity, float efficiency);
        public abstract bool CanBeOverTimeUpgrade(Rarity rarity, float efficiency);
        public abstract TableText GetMainDescription(Rarity rarity, Efficiency efficiency);
        public abstract string GetBonusDescription(Rarity rarity, Efficiency efficiency);
    }
}