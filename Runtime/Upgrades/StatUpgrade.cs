using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public class StatUpgrade : Upgrade
    {
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
        private Stat stat;

        private float efficiency;

        public UpgradeInfo UpgradeInfo
        {
            get
            {
                return Stat.UpgradeInfo;
            }
        }

        public StatUpgrade(Stat stat, Rarity rarity, int tickets, float efficiency) : base(rarity, tickets)
        {
            this.Stat = stat;
            this.efficiency = efficiency;
        }

        public override string GetDescription()
        {
            return UpgradeInfo.GetMainDescription(Stat, rarity);
        }
        public override string GetExtraDescription()
        {
            return UpgradeInfo.GetBonusDescription(Stat, rarity);
        }

        public override void Apply()
        {
            Stat.Upgrade(rarity, efficiency);
        }
    }
}