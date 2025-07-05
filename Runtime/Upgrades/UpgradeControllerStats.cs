using System.Collections.Generic;
using HexTecGames.Basics;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public class UpgradeControllerStats : CloneableStatCollection<UpgradeControllerStats>, ITicket
    {
        public int Tickets
        {
            get
            {
                return tickets;
            }
            private set
            {
                tickets = value;
            }
        }
        [SerializeField] private int tickets;

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

        public Stat TurnsPerReroll
        {
            get
            {
                return turnsPerReroll;
            }
            private set
            {
                turnsPerReroll = value;
            }
        }
        [SerializeField] private Stat turnsPerReroll = new Stat();
        public Stat CurrentRerollTicks
        {
            get
            {
                return currentRerollTicks;
            }
            private set
            {
                currentRerollTicks = value;
            }
        }
        [SerializeField] private Stat currentRerollTicks = new Stat();

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

        public Stat OverTimeChance
        {
            get
            {
                return overTimeChance;
            }
            private set
            {
                overTimeChance = value;
            }
        }
        [SerializeField] private Stat overTimeChance = new Stat();

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



        public StatUpgradeType RollUpgradeType(bool allowOverTime, bool allowMulti)
        {
            if (allowOverTime && OverTimeChance.Value > Random.Range(0, 100))
            {
                return StatUpgradeType.OverTime;
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
            efficiency.Add(currentRarity, currentRarity.GetMultiplier(), 0);
            efficiency.Add(AllEfficiency, 1);
            if (statUpgradeType == StatUpgradeType.OverTime)
            {
                efficiency.Add(OverTimeEfficiency, 2);
            }
            else if (statUpgradeType == StatUpgradeType.Single)
            {
                efficiency.Add(SingleEfficiency, 2);
            }
            else efficiency.Add(MultiEfficiency, 2);
            return efficiency;
        }

        protected override void AddStatsToList(List<Stat> stats)
        {
            stats.Add(TotalUpgrades);
            stats.Add(TurnsPerReroll);
            stats.Add(CurrentRerollTicks);

            stats.Add(AllEfficiency);
            stats.Add(SingleEfficiency);
            stats.Add(MultiEfficiency);
            stats.Add(OverTimeEfficiency);

            stats.Add(OverTimeChance);
            stats.Add(MultiChance);

            stats.Add(DoubleUpgradeChance);
        }


        protected override void CopyStats(UpgradeControllerStats target)
        {
            target.Tickets = Tickets;

            base.CopyStats(target);
        }
        public override UpgradeControllerStats CreateCopy()
        {
            UpgradeControllerStats clone = new UpgradeControllerStats();
            CopyStats(clone);
            return clone;
        }
    }
}