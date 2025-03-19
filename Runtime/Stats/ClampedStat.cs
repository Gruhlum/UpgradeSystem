using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public class ClampedStat : Stat
    {
        public override int Value
        {
            set
            {
                base.Value = ClampValue(value);
            }
        }

        private int minValue;
        private int maxValue;
        private Stat minStat;
        private Stat maxStat;

        private StatClamp minClamp;
        private StatClamp maxClamp;


        public ClampedStat(StatType statType) : base(statType)
        {
        }

        protected override Stat InstantiateCopy()
        {
            return new ClampedStat(StatType);
        }
        protected override void CopyValues(Stat stat)
        {
            base.CopyValues(stat);
            if (stat is ClampedStat clampedClone)
            {
                clampedClone.minValue = minValue;
                clampedClone.maxValue = maxValue;
                clampedClone.minStat = minStat;
                clampedClone.maxStat = maxStat;
                clampedClone.minClamp = minClamp;
                clampedClone.maxClamp = maxClamp;
            }
        }
        public override void ApplyData(List<Stat> allStats, StatData statData)
        {
            base.ApplyData(allStats, statData);

            if (statData.minValue.clampType == StatClamp.Value)
            {
                SetMinValue(statData.minValue.value);
            }
            else if (statData.minValue.clampType == StatClamp.Stat)
            {
                Stat stat = allStats.Find(x => x.StatType == statData.minValue.statType);
                SetMinValue(stat);
            }

            if (statData.maxValue.clampType == StatClamp.Value)
            {
                SetMaxValue(statData.maxValue.value);
            }
            else if (statData.maxValue.clampType == StatClamp.Stat)
            {
                Stat stat = allStats.Find(x => x.StatType == statData.maxValue.statType);
                SetMaxValue(stat);
            }
        }

        private void RemoveEvents(ref Stat stat)
        {
            stat.OnValueChanged -= Stat_OnValueChanged;
        }
        private void AddEvents(ref Stat stat)
        {
            stat.OnValueChanged += Stat_OnValueChanged;
        }
        private int ClampValue(int value)
        {
            if (minClamp != StatClamp.None && value < minValue)
            {
                value = minValue;
            }
            if (maxClamp != StatClamp.None && value > maxValue)
            {
                value = maxValue;
            }
            return value;
        }
        private void Stat_OnValueChanged(Stat stat, int value)
        {
            if (minStat == stat)
            {
                minValue = value;
            }
            else if (maxStat == stat)
            {
                maxValue = value;
            }
            Value = ClampValue(value);
        }

        public void SetMinValue(int minValue)
        {
            minClamp = StatClamp.Value;
            this.minValue = minValue;
            if (minStat != null)
            {
                RemoveEvents(ref minStat);
                minStat = null;
            }
        }
        public void SetMinValue(Stat minStat)
        {
            this.minStat = minStat;
            minClamp = StatClamp.Stat;
            minValue = minStat.Value;
            AddEvents(ref minStat);
        }
        public void SetMaxValue(int maxValue)
        {
            maxClamp = StatClamp.Value;
            this.maxValue = maxValue;

            if (maxStat != null)
            {
                RemoveEvents(ref maxStat);
                maxStat = null;
            }
        }
        public void SetMaxValue(Stat maxStat)
        {
            maxClamp = StatClamp.Stat;
            maxValue = maxStat.Value;
            this.maxStat = maxStat;
            AddEvents(ref this.maxStat);
        }
    }
}