using System;
using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public class StatUpgradeMaster : UpgradeMaster<StatUpgrade>
    {
        private StatCollection statCollection;
        private UpgradeControllerStats upgradeStats;

        public StatUpgradeMaster(string name, int tickets, StatCollection statCollection, UpgradeControllerStats upgradeStats)
            : base(name, tickets)
        {
            this.statCollection = statCollection;
            this.upgradeStats = upgradeStats;

        }

        public void LevelUp()
        {
            statCollection.LevelUp();
        }

        public bool HasMultiUpgrade(float efficiency, int totalRequired)
        {
            int count = 0;
            foreach (var item in upgrades)
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
        public bool HasOverTimeUpgrade(float efficiency, int totalRequired)
        {
            int count = 0;
            foreach (var item in upgrades)
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
            Efficiency overTimeEfficiency = upgradeStats.GetEfficiency(currentRarity, StatUpgradeType.OverTime);
            Efficiency multiEfficiency = upgradeStats.GetEfficiency(currentRarity, StatUpgradeType.Multi);

            bool canBeOverTime = HasOverTimeUpgrade(overTimeEfficiency.Total, 1);
            bool canBeMulti = HasMultiUpgrade(multiEfficiency.Total, 2);

            StatUpgradeType upgradeType = upgradeStats.RollUpgradeType(canBeOverTime, canBeMulti);
            //Debug.Log($"{nameof(upgradeType)} {upgradeType}");
            if (upgradeType == StatUpgradeType.OverTime)
            {
                return GenerateOverTimeUpgrade(currentRarity, overTimeEfficiency);
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
            Efficiency multiEfficiency = upgradeStats.GetEfficiency(rarity, StatUpgradeType.Multi);

            foreach (var stat in statCollection.Stats)
            {
                if (stat.IsValidUpgrade(rarity))
                {
                    upgrades.Add(stat.GetUpgrade(rarity, singleEfficiency, multiEfficiency));
                }
            }
            return upgrades;
        }
        private OverTimeUpgrade GenerateOverTimeUpgrade(Rarity rarity, Efficiency efficiency)
        {
            List<StatUpgrade> availableStats = GetPossibleOverTimeUpgrades(rarity, efficiency.Total);
            StatUpgrade result = ITicket.Roll(availableStats);
            upgrades.Remove(result);
            return new OverTimeUpgrade(result.Stat, rarity, efficiency, 100);
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
            foreach (var result in results)
            {
                SingleUpgrade upgrade = new SingleUpgrade(result, efficiency, targetRarity, 0);
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

            foreach (var upgrade in upgrades)
            {
                if (upgrade.Stat.CanBeMultiUpgrade(rarity, efficiency))
                {
                    availableStats.Add(upgrade);
                }
            }
            return availableStats;
        }
        private List<StatUpgrade> GetPossibleOverTimeUpgrades(Rarity rarity, float efficiency)
        {
            List<StatUpgrade> availableStats = new List<StatUpgrade>();

            foreach (var upgrade in upgrades)
            {
                if (upgrade.Stat.CanBeOverTimeUpgrade(rarity, efficiency))
                {
                    availableStats.Add(upgrade);
                }
            }
            return availableStats;
        }
    }
}