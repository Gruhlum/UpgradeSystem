using System;
using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics.UI;
using HexTecGames.UpgradeSystem;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public class NormalUpgrade : UpgradeEffect
    {
        public int increase = 10;

        public override StatUpgrade GetUpgrade(Stat stat, Rarity rarity, Efficiency singleEfficiency)
        {
            return new SingleUpgrade(stat, this, rarity, singleEfficiency, tickets);
        }
        public override void Apply(Rarity rarity, float efficiency)
        {
            throw new NotImplementedException();
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
        public override TableText GetMainDescription(Rarity rarity, Efficiency efficiency)
        {
            throw new NotImplementedException();
        }
        public override string GetBonusDescription(Rarity rarity, Efficiency efficiency)
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