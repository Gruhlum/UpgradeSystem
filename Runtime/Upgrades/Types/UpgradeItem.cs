using System;
using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics;
using HexTecGames.Basics.UI;
using HexTecGames.UpgradeSystem;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public class UpgradeItem
    {
        public StatType statType;

        [SubclassSelector, SerializeReference] public UpgradeEffect upgradeEffect;

        public SingleUpgrade GetValidUpgrade(StatCollection stats, Rarity rarity, Efficiency effieciency)
        {
            Stat stat = stats.Find(statType);

            if (upgradeEffect.IsValidUpgrade(stat, rarity, effieciency.Total))
            {
                return new SingleUpgrade(stat, upgradeEffect, rarity, effieciency);
            }
            else return null;
        }
    }
}