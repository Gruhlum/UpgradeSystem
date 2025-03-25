using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public class TotalCondition : Condition
    {
        [SerializeField] private int maximumUpgrades = 1;

        public override bool IsValid(Stat stat, Rarity rarity, List<Stat> allStats)
        {
            return maximumUpgrades > stat.UpgradeInfo.TotalUpgrades;
        }

        protected override void CopyTo(Condition condition)
        {
            if (condition is TotalCondition totalCondition)
            {
                totalCondition.maximumUpgrades = maximumUpgrades;
            }
        }

        protected override Condition InstantiateCondition()
        {
            return new TotalCondition();
        }
    }
}