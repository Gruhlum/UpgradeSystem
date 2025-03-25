using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public class Upgrade
    {
        public Stat stat;
        public Rarity rarity;
        public int tickets;
        private UpgradeInfo upgradeInfo;

        public Upgrade(UpgradeInfo upgradeInfo, Stat stat, Rarity rarity, int tickets)
        {
            this.upgradeInfo = upgradeInfo;
            this.stat = stat;
            this.rarity = rarity;
            this.tickets = tickets;
        }

        public string GetDescription()
        {
            return upgradeInfo.GetMainDescription(stat, rarity);
        }
        public string GetExtraDescription()
        {
            return upgradeInfo.GetBonusDescription(stat, rarity);
        }

        public void Apply()
        {
            stat.Upgrade(rarity);
        }
    }
}