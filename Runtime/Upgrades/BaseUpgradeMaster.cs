using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public abstract class BaseUpgradeMaster : ITicket
    {
        public string Name
        {
            get
            {
                return name;
            }
            private set
            {
                name = value;
            }
        }
        private string name;


        public int Tickets
        {
            get
            {
                return tickets;
            }
            private set
            {
                tickets = value;
            }
        }
        private int tickets;

        private List<Upgrade> upgrades = new List<Upgrade>();

        protected BaseUpgradeMaster(string name, int tickets)
        {
            this.Name = name;
            this.Tickets = tickets;
        }

        public Upgrade RollUpgrade()
        {
            Upgrade upgrade = ITicket.Roll(upgrades);
            upgrades.Remove(upgrade);
            return upgrade;
        }

        public bool HasUpgrade()
        {
            return upgrades != null && upgrades.Count > 0;
        }
        public void RefreshUpgrades(Rarity rarity)
        {
            upgrades = GenerateUpgrades(rarity);
        }
        protected abstract List<Upgrade> GenerateUpgrades(Rarity rarity);
        public void ClearUpgrades()
        {
            upgrades.Clear();
        }
    }
}