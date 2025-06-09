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
        public UpgradeInfo UpgradeInfo
        {
            get
            {
                return Stat.UpgradeInfo;
            }
        }

        public Efficiency Efficiency
        {
            get
            {
                return this.efficiency;
            }

            set
            {
                this.efficiency = value;
            }
        }
        private Efficiency efficiency;


        public SingleUpgrade(Stat stat, Efficiency efficiency, Rarity rarity, int tickets) : base(stat, rarity, tickets)
        {
            this.Efficiency = efficiency;
        }

        public override TableText GetDescription()
        {
            return UpgradeInfo.GetMainDescription(Stat, rarity, Efficiency);
        }
        public override string GetExtraDescription()
        {
            return UpgradeInfo.GetBonusDescription(Stat, rarity);
        }

        public override void Apply()
        {
            Stat.Upgrade(rarity, Efficiency.Total);
        }
    }
}