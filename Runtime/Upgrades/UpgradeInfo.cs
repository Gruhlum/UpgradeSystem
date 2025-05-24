using System;
using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public class UpgradeInfo
    {
        [SerializeField] private UpgradeType upgradeType;
        [SerializeField, DrawIf(nameof(upgradeType), UpgradeType.RarityIncrease)] private StatType multiStatType = default;
        public Rarity Rarity
        {
            get
            {
                return rarity;
            }
            set
            {
                rarity = value;
            }
        }
        [SerializeField]//, DrawIf(nameof(upgradeType), UpgradeType.None, reverse: true)]
        private Rarity rarity;
        public int Increase
        {
            get
            {
                return increase;
            }
            set
            {
                increase = value;
            }
        }
        [SerializeField]//, DrawIf(nameof(upgradeType), UpgradeType.None, reverse: true)]
        private int increase;

        public int Tickets
        {
            get
            {
                return tickets;
            }
            set
            {
                tickets = value;
            }
        }
        [SerializeField]//, DrawIf(nameof(upgradeType), UpgradeType.None, reverse: true)]
        private int tickets = 100;
        [SerializeField]//, DrawIf(nameof(upgradeType), UpgradeType.None, reverse: true)]
        [SubclassSelector, SerializeReference] private Condition condition;

        public int TotalUpgrades
        {
            get
            {
                return totalUpgrades;
            }
            protected set
            {
                totalUpgrades = value;
            }
        }

        public UpgradeType UpgradeType
        {
            get
            {
                return this.upgradeType;
            }
            set
            {
                this.upgradeType = value;
            }
        }

        private Stat multiUpgradeStat;
        private int totalUpgrades;


        public UpgradeInfo Create(Stat stat, List<Stat> allStats)
        {
            if (multiStatType == null && upgradeType == UpgradeType.RarityIncrease)
            {
                Debug.Log("No MultiStatType: " + stat.StatType);
            }
            if (this.Rarity == null && upgradeType == UpgradeType.RarityIncrease)
            {
                Debug.Log("No Rarity: " + stat.StatType);
            }

            UpgradeInfo clone = new UpgradeInfo();
            clone.UpgradeType = this.UpgradeType;
            clone.Rarity = this.Rarity;
            clone.Increase = this.Increase;
            clone.Tickets = this.Tickets;
            clone.multiStatType = this.multiStatType;
           
            if (multiStatType != null)
            {
                clone.multiUpgradeStat = allStats.Find(multiStatType);
            }
            if (this.condition != null)
            {
                clone.condition = this.condition.Create(allStats);
            }
            return clone;
        }
        public void ApplyUpgrade(Stat stat, Rarity rarity, float efficiency)
        {
            if (UpgradeType == UpgradeType.None)
            {
                return;
            }
            TotalUpgrades++;
            stat.FlatValue += Mathf.RoundToInt(GetUpgradeValue(stat, rarity) * efficiency);
        }
        public int GetUpgradeValue(Stat stat, Rarity rarity)
        {
            if (UpgradeType == UpgradeType.Flat || UpgradeType == UpgradeType.Normal)
            {
                int multiplier = CalculateMultiplier(stat, rarity);

                return increase * multiplier;
            }
            if (UpgradeType == UpgradeType.Percent)
            {
                return Mathf.RoundToInt(stat.FlatValue * (increase / 100f));
            }
            if (UpgradeType == UpgradeType.RarityIncrease)
            {
                int rarityDifference = rarity - this.Rarity.GetRarity(TotalUpgrades);
                return increase * (1 + (rarityDifference / 2));
            }
            else return increase;
        }

        private int CalculateMultiplier(Stat stat, Rarity rarity)
        {
            if (this.Rarity == null)
            {
                Debug.Log("No rarity for Stat: " + stat.StatType.name);
                return rarity.GetMultiplier(rarity.GetRarityByIndex(0));
            }
            return rarity.GetMultiplier(this.Rarity);
        }

        public Upgrade GetUpgrade(Stat stat, Rarity rarity)
        {
            StatUpgrade upgrade = new StatUpgrade(stat, rarity, Tickets, 1);

            if (upgradeType == UpgradeType.RarityIncrease)
            {
                int rarityDifference = this.Rarity.GetRarity(TotalUpgrades) - rarity;

                if (rarityDifference == 0)
                {
                    return upgrade;
                }
                else
                {
                    StatUpgrade secondUpgrade = (StatUpgrade)multiUpgradeStat.GetUpgrade(rarity.GetRarityByIndex(0));
                    return new MultiStatUpgrade(new List<StatUpgrade>() { upgrade, secondUpgrade }, rarity, Tickets);
                }
            }
            else return upgrade;
        }
        public bool IsValidUpgrade(Stat stat, Rarity rarity)
        {
            if (UpgradeType == UpgradeType.None)
            {
                return false;
            }
            if (this.Rarity != null && this.Rarity > rarity)
            {
                return false;
            }
            if (UpgradeType == UpgradeType.RarityIncrease && this.Rarity.GetRarity(TotalUpgrades) > rarity)
            {
                return false;
            }
            if (stat.MaxValue != null && stat.FlatValue + GetUpgradeValue(stat, rarity) > stat.MaxValue.value)
            {
                return false;
            }
            if (condition != null && !condition.IsValid(stat, rarity))
            {
                return false;
            }
            return true;
        }

        public string GetMainDescription(Stat stat, Rarity rarity)
        {
            return $"{stat.StatType.name}{Environment.NewLine}+{GetUpgradeValue(stat, rarity).ToString(stat.StatType.Formatting)}";
        }
        public string GetBonusDescription(Stat stat, Rarity rarity)
        {
            return string.Empty;
        }

        public bool GetBeMultiUpgrade()
        {
            return UpgradeType != UpgradeType.RarityIncrease;
        }
    }
}