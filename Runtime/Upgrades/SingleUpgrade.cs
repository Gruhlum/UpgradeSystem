using HexTecGames.Basics.UI;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public class SingleUpgrade : StatUpgrade
    {
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


        public SingleUpgrade(Stat stat, UpgradeEffect upgradeItem, Rarity rarity, Efficiency efficiency) : base(stat, upgradeItem, rarity)
        {
            this.Efficiency = efficiency;
        }

        public override TableText GetDescription()
        {
            return UpgradeEffect.GetMainDescription(this);
        }
        public override TableText GetExtraDescription()
        {
            return UpgradeEffect.GetBonusDescription(this);
        }

        public override void Apply()
        {
            UpgradeEffect.Apply(this);
        }
    }
}