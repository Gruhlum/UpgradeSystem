using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public class StatUpgrade : Upgrade
    {
        public Stat stat;
        
        private UpgradeInfo upgradeInfo;

        public StatUpgrade(UpgradeInfo upgradeInfo, Stat stat, Rarity rarity, int tickets) : base(rarity, tickets)
        {
            this.upgradeInfo = upgradeInfo;
            this.stat = stat;
        }

        public override string GetDescription()
        {
            return upgradeInfo.GetMainDescription(stat, rarity);
        }
        public override string GetExtraDescription()
        {
            return upgradeInfo.GetBonusDescription(stat, rarity);
        }

        public override void Apply()
        {
            stat.Upgrade(rarity);
        }
    }
}