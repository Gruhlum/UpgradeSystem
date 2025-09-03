using HexTecGames.Basics;
using HexTecGames.Basics.UI;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public abstract class Upgrade : ITicket
    {
        public Rarity rarity;

        public virtual int Tickets
        {
            get
            {
                return tickets;
            }
            set
            {
                tickets = value;
            }
        }
        private int tickets;

        protected Upgrade(Rarity rarity, int tickets)
        {
            this.rarity = rarity;
            this.Tickets = tickets;
        }
        public abstract TableText GetDescription();

        public abstract TableText GetExtraDescription();

        public abstract void Apply();
    }
}