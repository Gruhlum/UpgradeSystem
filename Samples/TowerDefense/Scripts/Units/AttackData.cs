namespace HexTecGames.UpgradeSystem.TowerDefense
{
    [System.Serializable]
    public class AttackData
    {
        public int damage;
        public bool isCrit;

        public AttackData(int damage, bool isCrit)
        {
            this.damage = damage;
            this.isCrit = isCrit;
        }
    }
}