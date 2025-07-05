using System.Collections.Generic;
using HexTecGames.Basics;
using HexTecGames.UpgradeSystem;

namespace HexTecGames
{
    public abstract class UpgradeMaster<T> : IUpgradeMaster where T : Upgrade
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

        public int AvailableUpgrades
        {
            get
            {
                return upgrades.Count;
            }
        }

        private int tickets;

        protected List<T> upgrades = new List<T>();
        protected Rarity currentRarity;


        public UpgradeMaster(string name, int tickets)
        {
            this.Name = name;
            this.Tickets = tickets;
        }

        public virtual Upgrade RollUpgrade()
        {
            Upgrade upgrade = ITicket.Roll(upgrades);
            upgrades.Remove(upgrade as T);
            return upgrade;
        }
        public bool HasUpgrade()
        {
            return upgrades != null && upgrades.Count > 0;
        }
        public void GenerateUpgrades(Rarity rarity)
        {
            ClearUpgrades();
            currentRarity = rarity;

            upgrades = CreateUpgrades(rarity);
        }

        protected abstract List<T> CreateUpgrades(Rarity rarity);

        public void ClearUpgrades()
        {
            upgrades.Clear();
        }
    }
}