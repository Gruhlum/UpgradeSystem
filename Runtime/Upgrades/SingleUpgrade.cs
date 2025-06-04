using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics;
using HexTecGames.Basics.UI;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public class SingleUpgrade : StatUpgrade
    {
        private Efficiency efficiency;

        public UpgradeInfo UpgradeInfo
        {
            get
            {
                return Stat.UpgradeInfo;
            }
        }

        public SingleUpgrade(Stat stat, Efficiency efficiency, Rarity rarity, int tickets) : base(stat, rarity, tickets)
        {
            this.efficiency = efficiency;
        }

        public override TableText GetDescription()
        {
            return UpgradeInfo.GetMainDescription(Stat, rarity, efficiency);
        }
        public override string GetExtraDescription()
        {
            return UpgradeInfo.GetBonusDescription(Stat, rarity);
        }

        public override void Apply()
        {
            Stat.Upgrade(rarity, efficiency.Total);
        }
    }
}