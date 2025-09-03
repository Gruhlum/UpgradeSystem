using System;
using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [CreateAssetMenu(menuName = "HexTecGames/UpgradeSystem/StatUpgradeData")]
    public class StatUpgradeData : ScriptableObject, ITicket
    {
        public StatCollectionDataBase statCollection;
        public List<UpgradeItem> upgradeItems;

        public int Tickets
        {
            get
            {
                return tickets;
            }
        }
        [SerializeField] private int tickets = 100;
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

        public IEnumerable<StatUpgrade> GetValidUpgrades(StatCollection stats, Rarity rarity, Efficiency singleEfficiency)
        {
            List<StatUpgrade> results = new List<StatUpgrade>();

            foreach (var upgradeItem in upgradeItems)
            {
                var result = upgradeItem.GetValidUpgrade(stats, rarity, singleEfficiency);
                if (result != null)
                {
                    results.Add(result);
                }
            }
            return results;
        }
    }
}