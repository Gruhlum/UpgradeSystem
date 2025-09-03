using System;
using HexTecGames.Basics.UI;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public abstract class StatUpgrade : Upgrade
    {
        private Stat stat;
        private UpgradeEffect upgradeEffect;

        public Stat Stat
        {
            get
            {
                return this.stat;
            }

            private set
            {
                this.stat = value;
            }
        }

        public UpgradeEffect UpgradeEffect
        {
            get
            {
                return this.upgradeEffect;
            }
            private set
            {
                this.upgradeEffect = value;
            }
        }

        protected StatUpgrade(Stat stat, UpgradeEffect upgradeEffect, Rarity rarity) : base(rarity, upgradeEffect.tickets)
        {
            this.Stat = stat;
            this.UpgradeEffect = upgradeEffect;
        }

        public bool CanBeMultiUpgrade(float efficiency)
        {
            return UpgradeEffect.CanBeMultiUpgrade(rarity, efficiency);
        }
        public bool CanBeOverTimeUpgrade(float efficiency)
        {
            // If rarity is Epic or Legendary
            if (rarity.GetIndex() > 1)
            {
                return false;
            }

            return UpgradeEffect.CanBeOverTimeUpgrade(rarity, efficiency);
        }

        public bool CanBeMultiUpgrade(Rarity rarity, float efficiency)
        {
            return UpgradeEffect.CanBeMultiUpgrade(rarity, efficiency);
        }

        public bool CanBeOverTimeUpgrade(Rarity rarity, float efficiency)
        {
            return UpgradeEffect.CanBeOverTimeUpgrade(rarity, efficiency);
        }
    }
}