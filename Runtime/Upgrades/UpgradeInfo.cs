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

        [SerializeField, DrawIf(nameof(upgradeType), UpgradeType.None, reverse: true)]
        private Rarity minRarity;

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
            clone.minRarity = this.minRarity;
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
                Rarity currentMinRarity = GetCurrentMinRarity(rarity);
                //Legendary = 3
                //Rare = 1;
                //TotalIndexes = 2;
                //Result: Increase by: IncreaseValue * 2

                int difference = rarity.GetIndex() - currentMinRarity.GetIndex();

                return increase * difference;
            }
            else return increase;
        }
        private Rarity GetCurrentMinRarity(Rarity rarity)
        {
            Rarity currentMinRarity;
            if (minRarity == null)
            {
                currentMinRarity = rarity.GetRarityByIndex(0);
            }
            else currentMinRarity = minRarity.GetRarity(TotalUpgrades);
            return currentMinRarity;
        }
        public Upgrade GetUpgrade(Stat stat, Rarity rarity, List<Stat> allStats)
        {
            if (!IsAllowedUpgrade(stat, rarity, allStats))
            {
                Debug.Log("in here");
                return null;
            }
            else return new Upgrade(this, stat, rarity, TotalTickets);
        }
        public bool IsAllowedUpgrade(Stat stat, Rarity rarity, List<Stat> allStats)
        {
            if (minRarity != null && minRarity.GetIndex() > rarity.GetIndex())
            {
                return false;               
            }
            if (!IsValidType(rarity))
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
        private bool IsValidType(Rarity rarity)
        {
            if (upgradeType == UpgradeType.Flat)
            {
                return true;
            }
            else
            {
                Rarity currentMinRarity = GetCurrentMinRarity(rarity);

                if (currentMinRarity.GetIndex() > rarity.GetIndex())
                {
                    return false;
                }
                return true;
            }
        }
        public string GetMainDescription(Stat stat, Rarity rarity)
        {
            return $"{stat.StatType.name}{Environment.NewLine}+{CalculateUpgradeIncrease(stat, rarity).ToString(stat.GetFormatting())}";
        }
        public string GetBonusDescription(Stat stat, Rarity rarity)
        {
            return string.Empty;
        }
    }
}