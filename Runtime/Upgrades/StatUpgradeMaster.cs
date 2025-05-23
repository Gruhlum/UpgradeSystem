using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public class StatUpgradeMaster : UpgradeMaster<Stat>
    {
        public StatUpgradeMaster(List<Stat> hasUpgrades, int tickets) : base(hasUpgrades, tickets)
        {
        }
    }
}