using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics;
using HexTecGames.Basics.UI;
using HexTecGames.UpgradeSystem;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public class RarityIncreaseUpgradeEffect : UpgradeEffect
    {
        public StatType secondaryStat;
        public int increase;

        public override StatUpgrade GetUpgrade(Stat stat, Rarity rarity, Efficiency singleEfficiency)
        {
            int totalUpgrades = (stat.FlatValue - stat.StartValue) / increase;
            SingleUpgrade upgrade = new SingleUpgrade(stat, this, rarity, singleEfficiency);

            float requiredMulti = this.rarity.GetRarity(totalUpgrades).GetMultiplier();
            float leftOverMulti = singleEfficiency.Total - requiredMulti;

            Efficiency leftOverEfficiency = new Efficiency();
            leftOverEfficiency.Add(0, new EfficiencyValue("Remaining", MathMode.Add, leftOverMulti));


            return upgrade;
            //if (multiUpgradeStat.UpgradeInfo.GetUpgradeValue(stat, rarity, leftOverEfficiency.Total) <= 0)
            //{
            //    return upgrade;
            //}
            //else
            //{
            //    SingleUpgrade secondUpgrade = multiUpgradeStat.GetUpgrade(rarity, leftOverEfficiency) as SingleUpgrade;
            //    return new MultiStatUpgrade(new List<SingleUpgrade>() { upgrade, secondUpgrade }, rarity, tickets);
            //}
        }

        public override bool CanBeMultiUpgrade(Rarity rarity, float efficiency)
        {
            return false;
        }

        public override bool CanBeOverTimeUpgrade(Rarity rarity, float efficiency)
        {
            return false;
        }

        public override bool IsValidUpgrade(Stat stat, Rarity rarity, float efficiency)
        {
            if ((this.rarity.GetIndex() + stat.TotalLevelUps) <= rarity.GetIndex())
            {
                return true;
            }
            return false;
        }

        protected override int CalculateIncrease(SingleUpgrade singleUpgrade)
        {
            int rarityDifference = singleUpgrade.rarity - rarity.GetRarity(singleUpgrade.Stat.TotalLevelUps);
            var result = increase * (1 + (rarityDifference / 2));
            return result;
        }
    }
}