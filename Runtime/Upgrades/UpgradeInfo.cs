using System.Collections.Generic;
using HexTecGames.Basics;
using HexTecGames.Basics.UI;
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

            UpgradeInfo clone = new UpgradeInfo
            {
                UpgradeType = this.UpgradeType,
                Rarity = this.Rarity,
                Increase = this.Increase,
                Tickets = this.Tickets,
                multiStatType = this.multiStatType
            };

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
        public int ApplyUpgrade(Stat stat, Rarity rarity, float efficiency)
        {
            if (UpgradeType == UpgradeType.None)
            {
                return 0;
            }
            TotalUpgrades++;
            int upgradeValue = GetUpgradeValue(stat, rarity, efficiency);
            stat.FlatValue += upgradeValue;
            return upgradeValue;
        }
        private int GetUpgradeValue(Stat stat, Rarity rarity)
        {
            if (UpgradeType == UpgradeType.Flat || UpgradeType == UpgradeType.Normal)
            {
                return increase;
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
        public int GetUpgradeValue(Stat stat, Rarity rarity, float efficiency)
        {
            if (upgradeType != UpgradeType.RarityIncrease)
            {
                return Mathf.RoundToInt(GetUpgradeValue(stat, rarity) * efficiency);
            }
            else return Mathf.RoundToInt(GetUpgradeValue(stat, rarity));
        }

        public StatUpgrade GetUpgrade(Stat stat, Rarity rarity, Efficiency singleEfficiency, Efficiency multiEfficiency)
        {
            SingleUpgrade upgrade = new SingleUpgrade(stat, singleEfficiency, rarity, Tickets);

            if (upgradeType == UpgradeType.RarityIncrease)
            {
                int rarityDifference = this.Rarity.GetRarity(TotalUpgrades) - rarity;

                if (rarityDifference == 0)
                {
                    return upgrade;
                }
                else
                {
                    Rarity targetRarity = rarity.GetRarity(-1);
                    SingleUpgrade secondUpgrade = multiUpgradeStat.GetUpgrade(targetRarity, singleEfficiency, multiEfficiency) as SingleUpgrade;
                    upgrade.Efficiency = singleEfficiency;
                    secondUpgrade.Efficiency = multiEfficiency;
                    return new MultiStatUpgrade(new List<SingleUpgrade>() { upgrade, secondUpgrade }, rarity, Tickets);
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

        public TableText GetMainDescription(Stat stat, Rarity rarity, Efficiency efficiency)
        {
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
            float upgradeValue = GetUpgradeValue(stat, rarity, efficiency.Total);
            string valuePrefix = upgradeValue > 0 ? "+" : null;
            string statValue = $"<link>{valuePrefix}{upgradeValue.ToString(stat.StatType.Formatting)}</link>"; // 4%

            SingleText s1 = new SingleText(statName, statTooltip);
            TextData tableData = new TextData(efficiency.GetTable());
            SingleText s2 = new SingleText(statValue, tableData);

            return new TableText(new MultiText(s1, s2));
        }
        public string GetBonusDescription(Stat stat, Rarity rarity)
        {
            return string.Empty;
        }

        public bool CanBeMultiUpgrade(Stat stat, Rarity rarity, float efficiency)
        {
            if (UpgradeType != UpgradeType.RarityIncrease)
            {
                return true;
            }
            if (rarity != this.Rarity.GetRarity(TotalUpgrades))
            {
                return true;
            }
            return false;
        }
        public bool CanBeOverTimeUpgrade(Stat stat, Rarity rarity, float efficiency)
        {
            if (UpgradeType == UpgradeType.RarityIncrease)
            {
                return false;
            }
            float upgradeValue = GetUpgradeValue(stat, rarity) * efficiency;
            if (upgradeValue <= 0.75f)
            {
                return false;
            }
            return true;
        }
    }
}