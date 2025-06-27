using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public class TotalCondition : Condition
    {
        [SerializeField] private int maximumUpgrades = 1;

        public TotalCondition(int maximumUpgrades)
        {
            this.maximumUpgrades = maximumUpgrades;
        }

        public override Condition Create(Dictionary<StatType, Stat> allStats)
        {
            return new TotalCondition(maximumUpgrades);
        }

        public override bool IsValid(Stat stat, Rarity rarity)
        {
            return maximumUpgrades > stat.UpgradeInfo.TotalUpgrades;
        }
    }
}