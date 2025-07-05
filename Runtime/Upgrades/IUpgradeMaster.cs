using HexTecGames.Basics;

namespace HexTecGames.UpgradeSystem
{
    public interface IUpgradeMaster : ITicket
    {
        string Name
        {
            get;
        }
        Upgrade RollUpgrade();
        void GenerateUpgrades(Rarity rarity);
        void ClearUpgrades();
        bool HasUpgrade();
    }
}