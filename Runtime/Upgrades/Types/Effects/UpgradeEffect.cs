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

        public void Apply(SingleUpgrade upgrade)
        {
            upgrade.Stat.FlatValue += CalculateIncrease(upgrade);
        }
        public virtual bool IsValidUpgrade(Stat stat, Rarity rarity, float efficiency)
        {
            if (this.rarity <= rarity)
            {
                return true;
            }
            return false;
        }
        public abstract StatUpgrade GetUpgrade(Stat stat, Rarity rarity, Efficiency singleEfficiency);
        public abstract bool CanBeMultiUpgrade(Rarity rarity, float efficiency);
        public abstract bool CanBeOverTimeUpgrade(Rarity rarity, float efficiency);
        protected abstract int CalculateIncrease(SingleUpgrade singleUpgrade);

        public virtual TableText GetBonusDescription(SingleUpgrade upgrade)
        {
            return GetMainDescription(upgrade);
        }
        public virtual TableText GetMainDescription(SingleUpgrade upgrade)
        {
            Stat stat = upgrade.Stat;
            Efficiency efficiency = upgrade.Efficiency;
            string statName = $"<link>{stat.StatType.name}</link>"; // Dodge Chance
            if (stat == null)
            {
                Debug.Log("Stat is null!");
            }
            else if (stat.StatType == null)
            {
                Debug.Log("StatType is null!");
            }

            string statTooltip = stat.StatType.Description; // Chance to completely avoid an Attack
            float upgradeValue = CalculateIncrease(upgrade);
            string valuePrefix = upgradeValue > 0 ? "+" : null;
            string statValue = $"<link>{valuePrefix}{upgradeValue.ToString(stat.StatType.Formatting)}</link>"; // 4%

            SingleText s1 = new SingleText(statName, statTooltip);
            TextData tableData = new TextData(efficiency.GetTable());
            SingleText s2 = new SingleText(statValue, tableData);

            return new TableText(new MultiText(s1, s2));
        }
    }
}