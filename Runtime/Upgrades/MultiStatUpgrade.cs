using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public class MultiStatUpgrade : Upgrade
    {
        private List<StatUpgrade> statUpgrades = new List<StatUpgrade>();

        public MultiStatUpgrade(List<StatUpgrade> statUpgrades, Rarity rarity, int tickets) : base(rarity, tickets)
        {
            this.statUpgrades = statUpgrades;
        }

        public override void Apply()
        {
            foreach (var upgrade in statUpgrades)
            {
                upgrade.Apply();
            }
        }

        public override string GetDescription()
        {
            List<string> results = new List<string>(statUpgrades.Count);

            foreach (var upgrade in statUpgrades)
            {
                results.Add(upgrade.GetDescription());
            }

            return string.Join(Environment.NewLine, results);
        }

        public override string GetExtraDescription()
        {
            List<string> results = new List<string>(statUpgrades.Count);

            foreach (var upgrade in statUpgrades)
            {
                results.Add(upgrade.GetExtraDescription());
            }

            return string.Join(Environment.NewLine, results);
        }
    }
}