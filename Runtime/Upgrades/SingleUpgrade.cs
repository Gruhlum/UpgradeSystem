using HexTecGames.Basics.UI;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public class SingleUpgrade : StatUpgrade
    {
        //public UpgradeInfo UpgradeInfo
        //{
        //    get
        //    {
        //        return Stat.UpgradeInfo;
        //    }
        //}

        public Efficiency Efficiency
        {
            get
            {
                return this.efficiency;
            }

            set
            {
                this.efficiency = value;
            }
        }
        private Efficiency efficiency;


        public SingleUpgrade(Stat stat, UpgradeEffect upgradeItem, Rarity rarity, Efficiency efficiency, int tickets)
            : base(stat, upgradeItem, rarity, tickets)
        {
            this.Efficiency = efficiency;
        }

        public override TableText GetDescription()
        {
            return upgradeItem.GetMainDescription(rarity, Efficiency);
        }
        public override string GetExtraDescription()
        {
            return upgradeItem.GetBonusDescription(rarity, Efficiency);
        }

        public override void Apply()
        {
            upgradeItem.Apply(rarity, Efficiency.Total);
        }
    }
}