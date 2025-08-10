using HexTecGames.Basics.UI;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public class OverTimeUpgrade : Upgrade
    {
        private Stat stat;
        private Efficiency efficiency;

        public OverTimeUpgrade(Stat stat, Rarity rarity, Efficiency efficiency, int tickets) : base(rarity, tickets)
        {
            this.stat = stat;
            this.efficiency = efficiency;
        }

        public override void Apply()
        {
            //stat.AddLevelUpValue(stat.UpgradeInfo.GetUpgradeValue(stat, rarity, efficiency.Total));
        }

        public override TableText GetDescription()
        {
            throw new System.NotImplementedException();
            //TableText result = stat.UpgradeInfo.GetMainDescription(stat, rarity, efficiency);
            //result.multiTexts.Add(new MultiText("Every Level Up"));
            //return result;
        }

        public override string GetExtraDescription()
        {
            throw new System.NotImplementedException();
        }
    }
}