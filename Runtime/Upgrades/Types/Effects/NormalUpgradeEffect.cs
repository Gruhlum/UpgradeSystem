using System;
using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics.UI;
using HexTecGames.UpgradeSystem;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public class NormalUpgradeEffect : UpgradeEffect
    {
        public int increase = 10;

        public override StatUpgrade GetUpgrade(Stat stat, Rarity rarity, Efficiency singleEfficiency)
        {
            return new SingleUpgrade(stat, this, rarity, singleEfficiency);
        }

        public override bool CanBeMultiUpgrade(Rarity rarity, float efficiency)
        {
            return true;
        }
        public override bool CanBeOverTimeUpgrade(Rarity rarity, float efficiency)
        {
            float upgradeValue = GetUpgradeValue(rarity) * efficiency;
            if (upgradeValue <= 0.75f)
            {
                return false;
            }
            return true;
        }

        protected override int CalculateIncrease(SingleUpgrade singleUpgrade)
        {
            return Mathf.RoundToInt(increase * rarity.GetMultiplier() * singleUpgrade.Efficiency.Total);
        }

        public override TableText GetBonusDescription(SingleUpgrade upgrade)
        {
            throw new NotImplementedException();
        }

        private float GetUpgradeValue(Rarity rarity)
        {
            return increase * rarity.GetMultiplier();
        }

        public override bool IsValidUpgrade(Stat stat, Rarity rarity, float efficiency)
        {
            return true;
        }
    }
}