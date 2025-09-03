using System.Collections.Generic;
using HexTecGames.Basics;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public class StatUpgradeMaster : UpgradeMaster<StatUpgrade>
    {
        private StatUpgradeData upgradeData;
        private UpgradeControllerStats upgradeStats;
        private StatCollection statCollection;


        public StatUpgradeMaster(StatCollection statCollection, StatUpgradeData upgradeData, UpgradeControllerStats upgradeStats) 
            : base(upgradeData.name, upgradeData.Tickets)
        {
            this.statCollection = statCollection;
            this.upgradeStats = upgradeStats;
            this.upgradeData = upgradeData;
        }

        public void LevelUp()
        {
            statCollection.LevelUp();
        }

        public bool HasMultiUpgrade(float efficiency, int totalRequired)
        {
            int count = 0;
            foreach (StatUpgrade item in upgrades)
            {
                if (item.CanBeMultiUpgrade(efficiency))
                {
                    count++;
                    if (count >= totalRequired)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool HasPerLevelUpgrade(float efficiency, int totalRequired)
        {
            int count = 0;
            foreach (StatUpgrade item in upgrades)
            {
                if (item.CanBeOverTimeUpgrade(efficiency))
                {
                    count++;
                    if (count >= totalRequired)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public override Upgrade RollUpgrade()
        {
            Efficiency perLevelEfficiency = upgradeStats.GetEfficiency(currentRarity, StatUpgradeType.PerLevel);
            Efficiency multiEfficiency = upgradeStats.GetEfficiency(currentRarity, StatUpgradeType.Multi);


            bool canBePerLevel = HasPerLevelUpgrade(perLevelEfficiency.Total, 1);
            //Debug.Log(perLevelEfficiency + " - " + canBePerLevel);
            bool canBeMulti = HasMultiUpgrade(multiEfficiency.Total, 2);

            StatUpgradeType upgradeType = upgradeStats.RollUpgradeType(canBePerLevel, canBeMulti);
            //Debug.Log($"{nameof(upgradeType)} {upgradeType}");
            if (upgradeType == StatUpgradeType.PerLevel)
            {
                return GeneratePerLevelUpgrade(currentRarity, perLevelEfficiency);
            }
            else if (upgradeType == StatUpgradeType.Multi)
            {
                return GenerateMultiUpgrade(currentRarity, multiEfficiency, 2);
            }
            else return base.RollUpgrade();
        }

        protected override List<StatUpgrade> CreateUpgrades(Rarity rarity)
        {
            List<StatUpgrade> upgrades = new List<StatUpgrade>();

            Efficiency singleEfficiency = upgradeStats.GetEfficiency(rarity, StatUpgradeType.Single);

            upgrades.AddRange(upgradeData.GetValidUpgrades(statCollection, rarity, singleEfficiency));
            return upgrades;
        }
        private OverTimeUpgrade GeneratePerLevelUpgrade(Rarity rarity, Efficiency efficiency)
        {
            List<StatUpgrade> availableStats = GetPossibleOverTimeUpgrades(rarity, efficiency.Total);
            StatUpgrade result = ITicket.Roll(availableStats);
            upgrades.Remove(result);
            return new OverTimeUpgrade(result.Stat, result.UpgradeEffect, rarity, efficiency);
        }

        public MultiStatUpgrade GenerateMultiUpgrade(Rarity rarity, Efficiency efficiency, int totalStats)
        {
            //Debug.Log(rarity + " - " + rarity.name + " " + rarity.GetMultiplier());
            List<StatUpgrade> availableStats = GetPossibleMultiUpgrades(rarity, efficiency.Total);
            List<Stat> results = RollMultiStats(totalStats, availableStats);
            List<SingleUpgrade> upgrades = GenerateUpgradesForMultiUpgrade(rarity, efficiency, results);

            return new MultiStatUpgrade(upgrades, rarity, 100);
        }

        private List<SingleUpgrade> GenerateUpgradesForMultiUpgrade(Rarity targetRarity, Efficiency efficiency, List<Stat> results)
        {
            List<SingleUpgrade> upgrades = new List<SingleUpgrade>();
            foreach (Stat result in results)
            {
                var upgradeItem = upgradeData.upgradeItems.Find(x => x.statType == result.StatType);
                SingleUpgrade upgrade = new SingleUpgrade(result, upgradeItem.upgradeEffect, targetRarity, efficiency);
                upgrades.Add(upgrade);
            }
            return upgrades;
        }

        private List<Stat> RollMultiStats(int totalStats, List<StatUpgrade> availableStats)
        {
            List<Stat> results = new List<Stat>();
            for (int i = 0; i < totalStats; i++)
            {
                StatUpgrade result = ITicket.Roll(availableStats);
                upgrades.Remove(result);
                availableStats.Remove(result);
                if (result == null)
                {
                    Debug.Log("Result is null! " + Name + " - " + i);
                }
                results.Add(result.Stat);
            }
            return results;
        }

        private List<StatUpgrade> GetPossibleMultiUpgrades(Rarity rarity, float efficiency)
        {
            List<StatUpgrade> availableStats = new List<StatUpgrade>();

            foreach (StatUpgrade upgrade in upgrades)
            {
                if (upgrade.CanBeMultiUpgrade(rarity, efficiency))
                {
                    availableStats.Add(upgrade);
                }
            }
            return availableStats;
        }
        private List<StatUpgrade> GetPossibleOverTimeUpgrades(Rarity rarity, float efficiency)
        {
            List<StatUpgrade> availableStats = new List<StatUpgrade>();

            foreach (StatUpgrade upgrade in upgrades)
            {
                if (upgrade.CanBeOverTimeUpgrade(rarity, efficiency))
                {
                    availableStats.Add(upgrade);
                }
            }
            return availableStats;
        }
    }
}