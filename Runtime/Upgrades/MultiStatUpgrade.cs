using System;
using System.Collections.Generic;
using HexTecGames.Basics.UI;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public class MultiStatUpgrade : StatUpgrade
    {
        private List<SingleUpgrade> statUpgrades = new List<SingleUpgrade>();

        public MultiStatUpgrade(List<SingleUpgrade> statUpgrades, Rarity rarity, int tickets) : base(statUpgrades[0].Stat, rarity, tickets)
        {
            this.statUpgrades = statUpgrades;
        }

        public override void Apply()
        {
            foreach (SingleUpgrade upgrade in statUpgrades)
            {
                upgrade.Apply();
            }
        }

        public override TableText GetDescription()
        {
            TableText results = new TableText();

            foreach (SingleUpgrade upgrade in statUpgrades)
            {
                results.multiTexts.AddRange(upgrade.GetDescription().multiTexts);
            }

            //results.multiTexts.Sort();

            return results;
        }
        public override string GetExtraDescription()
        {
            List<string> results = new List<string>(statUpgrades.Count);

            foreach (SingleUpgrade upgrade in statUpgrades)
            {
                results.Add(upgrade.GetExtraDescription());
            }

            return string.Join(Environment.NewLine, results);
        }
    }
}