namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public abstract class StatUpgrade : Upgrade
    {
        protected Stat stat;
        protected UpgradeItem upgradeItem;

        protected StatUpgrade(Stat stat, UpgradeItem upgradeItem, Rarity rarity, int tickets) : base(rarity, tickets)
        {
            this.stat = stat;
            this.upgradeItem = upgradeItem;
        }

        public bool CanBeMultiUpgrade(float efficiency)
        {
            return upgradeItem.CanBeMultiUpgrade(rarity, efficiency);
        }
        public bool CanBeOverTimeUpgrade(float efficiency)
        {
            // If rarity is Epic or Legendary
            if (rarity.GetIndex() > 1)
            {
                return false;
            }

            return upgradeItem.CanBeOverTimeUpgrade(rarity, efficiency);
        }
    }
}