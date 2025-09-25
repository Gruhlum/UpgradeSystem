using System.Collections.Generic;
using HexTecGames.Basics;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public class UpgradeControllerStats : CloneableStatCollection<UpgradeControllerStats>
    {
        public Stat TotalUpgrades
        {
            get
            {
                return this.totalUpgrades;
            }

            set
            {
                this.totalUpgrades = value;
            }
        }
        [SerializeField] private Stat totalUpgrades = new Stat();

        public Stat AllEfficiency
        {
            get
            {
                return allEfficiency;
            }
            private set
            {
                allEfficiency = value;
            }
        }
        [SerializeField] private Stat allEfficiency = new Stat();

        public Stat SingleEfficiency
        {
            get
            {
                return singleEfficiency;
            }
            private set
            {
                singleEfficiency = value;
            }
        }
        [SerializeField] private Stat singleEfficiency = new Stat();

        public Stat MultiEfficiency
        {
            get
            {
                return multiEfficiency;
            }
            private set
            {
                multiEfficiency = value;
            }
        }
        [SerializeField] private Stat multiEfficiency = new Stat();

        public Stat OverTimeEfficiency
        {
            get
            {
                return overTimeEfficiency;
            }
            private set
            {
                overTimeEfficiency = value;
            }
        }
        [SerializeField] private Stat overTimeEfficiency = new Stat();

        public Stat PerLevelEfficiency
        {
            get
            {
                return perLevelEfficiency;
            }
            private set
            {
                perLevelEfficiency = value;
            }
        }
        [SerializeField] private Stat perLevelEfficiency = new Stat();

        public Stat PerLevelChance
        {
            get
            {
                return perLevelChance;
            }
            private set
            {
                perLevelChance = value;
            }
        }
        [SerializeField] private Stat perLevelChance = new Stat();

        public Stat MultiChance
        {
            get
            {
                return multiChance;
            }
            private set
            {
                multiChance = value;
            }
        }
        [SerializeField] private Stat multiChance = new Stat();

        public Stat DoubleUpgradeChance
        {
            get
            {
                return doubleUpgradeChance;
            }
            private set
            {
                doubleUpgradeChance = value;
            }
        }
        [SerializeField] private Stat doubleUpgradeChance = new Stat();



        public StatUpgradeType RollUpgradeType(bool allowPerLevel, bool allowMulti)
        {
            if (allowPerLevel && PerLevelChance.Value > Random.Range(0, 100))
            {
                return StatUpgradeType.PerLevel;
            }
            if (allowMulti && MultiChance.Value > Random.Range(0, 100))
            {
                return StatUpgradeType.Multi;
            }
            else return StatUpgradeType.Single;
        }
        public Efficiency GetEfficiency(Rarity currentRarity, StatUpgradeType statUpgradeType)
        {
            Efficiency efficiency = new Efficiency();
            efficiency.Add(0, currentRarity.name, MathMode.Add, currentRarity.GetMultiplier());
            efficiency.Add(1, AllEfficiency, MathMode.Multiply);

            if (statUpgradeType == StatUpgradeType.PerLevel)
            {
                efficiency.Add(2, PerLevelEfficiency, MathMode.Multiply);
                //Debug.Log(PerLevelEfficiency);
            }
            else if (statUpgradeType == StatUpgradeType.Single)
            {
                efficiency.Add(2, SingleEfficiency, MathMode.Multiply);
            }
            else efficiency.Add(2, MultiEfficiency, MathMode.Multiply);

            efficiency.Add(3, OverTimeEfficiency, MathMode.Multiply);
            return efficiency;
        }

        protected override void AddStatsToList(List<Stat> stats)
        {
            stats.Add(TotalUpgrades);

            stats.Add(AllEfficiency);
            stats.Add(SingleEfficiency);
            stats.Add(MultiEfficiency);
            stats.Add(OverTimeEfficiency);

            stats.Add(PerLevelEfficiency);
            stats.Add(PerLevelChance);
            stats.Add(MultiChance);

            stats.Add(DoubleUpgradeChance);
        }


        protected override UpgradeControllerStats InstantiateCopy()
        {
            return new UpgradeControllerStats();
        }
    }
}