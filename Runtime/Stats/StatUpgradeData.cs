using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{    
    [CreateAssetMenu(menuName = "HexTecGames/UpgradeSystem/StatUpgradeData")]
    public class StatUpgradeData : ScriptableObject
    {
        public List<UpgradeItem> upgradeItems;

        public int GetTotalTickets()
        {
            if (upgradeItems == null)
            {
                return 0;
            }

            int totalTickets = 0;

            foreach (var item in upgradeItems)
            {
                if (item.upgradeEffect != null)
                {
                    totalTickets += item.upgradeEffect.tickets;
                }
            }

            return totalTickets;
        }
    }
}