using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{    
    [CreateAssetMenu(menuName = "HexTecGames/UpgradeSystem/StatUpgradeData")]
    public class StatUpgradeData : ScriptableObject
    {
        public StatCollectionDataBase statCollection;
        public List<UpgradeItem> upgradeItems;

        public List<Stat> GetStats()
        {
            if (statCollection == null)
            {
                return null;
            }
            return statCollection.GetStats();
        }

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