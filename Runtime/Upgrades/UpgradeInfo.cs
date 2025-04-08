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

        public int Increase
        {
            get
            {
                return increase;
            }
            private set
            {
                increase = value;
            }
        }
        [SerializeField, DrawIf(nameof(upgradeType), UpgradeType.None, reverse: true)]
        private int increase;

        public int TotalTickets
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
        [SerializeField, DrawIf(nameof(upgradeType), UpgradeType.None, reverse: true)]
        private int tickets = 100;
        [SerializeField, DrawIf(nameof(upgradeType), UpgradeType.None, reverse: true)]
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
        private int totalUpgrades;


        public UpgradeInfo CreateCopy()
        {
            UpgradeInfo clone = new UpgradeInfo();
            clone.upgradeType = this.upgradeType;
            clone.Increase = this.Increase;
            clone.TotalTickets = this.TotalTickets;
            clone.condition = this.condition.CreateCopy();
            return clone;
        }
        public void ApplyUpgrade(Stat stat, Rarity rarity)
        {
            TotalUpgrades++;
            stat.FlatValue += GetUpgradeValue(stat, rarity);
        }
        public int GetUpgradeValue(Stat stat, Rarity rarity)
        {
            if (upgradeType == UpgradeType.Flat)
            {
                return increase;
            }
            else return CalculateUpgradeIncrease(stat, rarity);
        }
        private int CalculateUpgradeIncrease(Stat stat, Rarity rarity)
        {
            if (upgradeType == UpgradeType.Flat || upgradeType == UpgradeType.Normal)
            {
                return increase;
            }
            if (upgradeType == UpgradeType.Percent)
            {
                return stat.FlatValue * increase;
            }
            if (upgradeType == UpgradeType.RarityIncrease)
            {
                //Rarity currentMinRarity = GetCurrentMinRarity(rarity);
                //Legendary = 3
                //Rare = 1;
                //TotalIndexes = 2;
                //Result: Increase by: IncreaseValue * 2

                //int difference = rarity.GetIndex() - currentMinRarity.GetIndex();

                return increase * 1;
            }
            else return increase;
        }

        public Upgrade GetUpgrade(Stat stat, Rarity rarity, List<Stat> allStats)
        {
            if (!IsAllowedUpgrade(stat, rarity, allStats))
            {
                Debug.Log("in here");
                return null;
            }
            else return new StatUpgrade(this, stat, rarity, TotalTickets);
        }
        public bool IsAllowedUpgrade(Stat stat, Rarity rarity, List<Stat> allStats)
        {
            if (upgradeType == UpgradeType.None)
            {
                return false;
            }
            if (!IsValidCondition(stat, rarity, allStats))
            {
                return false;
            }
            if (TotalTickets <= 0)
            {
                return false;
            }
            return true;
        }
        private bool IsValidCondition(Stat stat, Rarity rarity, List<Stat> allStats)
        {
            if (condition == null)
            {
                return true;
            }

            return condition.IsValid(stat, rarity, allStats);
        }

        public string GetMainDescription(Stat stat, Rarity rarity)
        {
            return $"{stat.StatType.name}{Environment.NewLine}+{CalculateUpgradeIncrease(stat, rarity).ToString(stat.StatType.Formatting)}";
        }
        public string GetBonusDescription(Stat stat, Rarity rarity)
        {
            return string.Empty;
        }
    }
}