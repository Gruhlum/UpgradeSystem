namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public class StatValue
    {
        public StatType statType;
        public int value;

        public StatValue(StatType statType)
        {
            this.statType = statType;
        }

        public StatValue(StatType statType, int value) : this(statType)
        {
            this.value = value;
        }


        public override string ToString()
        {
            return $"{statType.name}: +{value.ToString(statType.Formatting)}";
        }
    }
}