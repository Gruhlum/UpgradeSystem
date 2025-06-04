using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    public interface IUpgradeMaster : ITicket
    {
        public string Name
        {
            get;
        }
        public Upgrade RollUpgrade();
        public void GenerateUpgrades(Rarity rarity);
        public void ClearUpgrades();
        public bool HasUpgrade();
    }
}