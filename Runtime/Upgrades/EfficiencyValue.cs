using HexTecGames.Basics;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public struct EfficiencyValue
    {
        public string type;
        public MathMode mode;
        public float value;

        public EfficiencyValue(string type, MathMode mode, float value)
        {
            this.value = value;
            this.mode = mode;
            this.type = type;
        }

        public float Apply(float input)
        {
            return mode.Apply(input, value);
        }

        public string GetItem()
        {
            return $"{type}: {value}";
        }
    }
}