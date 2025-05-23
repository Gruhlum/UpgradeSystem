using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public class ClampValue
    {
        public int value;
        [SerializeField, SerializeReference] private Stat stat;
        public StatType statType;
        [SerializeField] private ClampType clampType;
        public StatClamp statClamp;

        public ClampValue(ClampType clampType, int value)
        {
            this.value = value;
            this.clampType = clampType;
        }
        public ClampValue(ClampType clampType, Stat stat) : this(clampType, stat.Value)
        {
            this.stat = stat;
            stat.OnValueChanged += Stat_OnValueChanged;
        }

        public void ValidateData(StatCollection statsCollection)
        {
            if (stat == null || stat.StatType != statType)
            {
                stat = statsCollection.Find(statType);
            }
        }

        private void Stat_OnValueChanged(Stat stat, int value)
        {
            this.value = value;
        }

        public int Clamp(int input)
        {
            if (clampType == ClampType.Max)
            {
                return Mathf.Min(input, value);
            }
            else return Mathf.Max(input, value);
        }

        public ClampValue CreateCopy()
        {
            ClampValue clone;
            if (stat != null)
            {
                clone = new ClampValue(clampType, stat);
            }
            else clone = new ClampValue(clampType, value);

            return clone;
        }
    }
}