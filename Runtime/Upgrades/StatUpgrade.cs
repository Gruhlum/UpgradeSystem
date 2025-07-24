namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public abstract class StatUpgrade : Upgrade
    {
        public Stat Stat
        {
            get
            {
                return this.stat;
            }
            private set
            {
                this.stat = value;
            }
        }
        private Stat stat;

        protected StatUpgrade(Stat stat, Rarity rarity, int tickets) : base(rarity, tickets)
        {
            this.Stat = stat;
        }

        public bool CanBeMultiUpgrade(float efficiency)
        {
            return Stat.CanBeMultiUpgrade(rarity, efficiency);
        }
        public bool CanBeOverTimeUpgrade(float efficiency)
        {
            // If rarity is Epic or Legendary
            if (rarity.GetIndex() > 1)
            {
                return false;
            }

            return Stat.CanBeOverTimeUpgrade(rarity, efficiency);
        }
    }
}