using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics;
using HexTecGames.UpgradeSystem;
using UnityEngine;

namespace HexTecGames
{
    public abstract class UpgradeMaster<T> : BaseUpgradeMaster where T : IHasUpgrade
    {
        protected List<T> hasUpgrades;


        public UpgradeMaster(List<T> hasUpgrades, int tickets) : base(tickets)
        {
            this.hasUpgrades = hasUpgrades;
        }

        protected override List<Upgrade> GenerateUpgrades(Rarity rarity)
        {
            List<Upgrade> upgrades = new List<Upgrade>();

            foreach (var hasUpgrade in hasUpgrades)
            {
                if (hasUpgrade.IsValidUpgrade(rarity))
                {
                    upgrades.Add(hasUpgrade.GetUpgrade(rarity));
                }
            }
            return upgrades;
        }
    }
}