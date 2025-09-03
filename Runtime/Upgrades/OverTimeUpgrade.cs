using HexTecGames.Basics.UI;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public class OverTimeUpgrade : SingleUpgrade
    {

        public OverTimeUpgrade(Stat stat, UpgradeEffect upgradeEffect, Rarity rarity, Efficiency efficiency) 
            : base(stat, upgradeEffect, rarity, efficiency)
        {

        }

        public override void Apply()
        {
            //stat.AddLevelUpValue(stat.UpgradeInfo.GetUpgradeValue(stat, rarity, efficiency.Total));
        }

        public override TableText GetDescription()
        {
            TableText result = base.GetDescription();
            result.multiTexts.Add(new MultiText("Every Level Up"));
            return result;
        }

        public override TableText GetExtraDescription()
        {
            throw new System.NotImplementedException();
        }
    }
}