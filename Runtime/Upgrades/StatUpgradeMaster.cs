using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public class StatUpgradeMaster : UpgradeMaster<Stat>
    {
        public StatUpgradeMaster(string name, int tickets, List<Stat> hasUpgrades) : base(name, tickets, hasUpgrades)
        {
        }
    }
}