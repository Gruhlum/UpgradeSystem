using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public class Stat
    {
        public StatType StatType
        {
            get
            {
                return statType;
            }
            private set
            {
                statType = value;
            }
        }
        [SerializeField] private StatType statType;

        public virtual int Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }
        private int value;
        public int UpgradeIncrease
        {
            get
            {
                return this.upgradeIncrease;
            }

            set
            {
                this.upgradeIncrease = value;
            }
        }
        private int upgradeIncrease;

        public string Formatting
        {
            get
            {
                return formatting;
            }
            private set
            {
                formatting = value;
            }
        }
        private string formatting;


        public event Action<Stat, int> OnValueChanged;

        public Stat(StatType statType)
        {
            this.StatType = statType;
        }

        public Stat CreateCopy()
        {
            Stat stat = InstantiateCopy();
            CopyValues(stat);
            return stat;
        }
        protected virtual Stat InstantiateCopy()
        {
            return new Stat(StatType);
        }
        protected virtual void CopyValues(Stat stat)
        {
            stat.Value = this.Value;
            stat.UpgradeIncrease = this.UpgradeIncrease;
            stat.Formatting = this.Formatting;
        }

        public virtual void ApplyData(List<Stat> allStats, StatData statData)
        {
            ApplyData(statData.startValue, statData.increase, statData.formatting);
        }
        public void ApplyData(int startValue, int upgradeIncrease, string formatting = null)
        {
            this.Value = startValue;
            this.UpgradeIncrease = upgradeIncrease;
            this.Formatting = formatting;
        }
        public bool IsUpgradeable()
        {
            return true;
        }
        public Upgrade GetUpgrade()
        {
            return new Upgrade(this, UpgradeIncrease);
        }

        public override string ToString()
        {
            return $"{StatType.name}: {Value.ToString(Formatting)}";
        }

        public string GetUpgradeDescription()
        {
            return $"{StatType.name}{Environment.NewLine} {Value.ToString(Formatting)} -> {(Value + upgradeIncrease).ToString(Formatting)}";
        }

        public static int operator +(Stat a, int value)
        {
            if (a == null)
            {
                return value;
            }
            return a.Value + value;
        }
        public static int operator +(int value, Stat a)
        {
            return a + value;
        }
        public static int operator -(Stat a, int value)
        {
            if (a == null)
            {
                return value;
            }
            return a.Value - value;
        }
        public static int operator -(int value, Stat a)
        {
            return a - value;
        }
        public static int operator *(Stat a, int value)
        {
            if (a == null)
            {
                return value;
            }
            return a.Value * value;
        }
        public static int operator *(int value, Stat a)
        {
            return a * value;
        }
        public static int operator /(Stat a, int value)
        {
            if (a == null)
            {
                return value;
            }
            return a.Value / value;
        }
        public static int operator /(int value, Stat a)
        {
            return a / value;
        }

        public static bool operator >(Stat a, int value)
        {
            if (a == null)
            {
                return false;
            }
            return a.Value > value;
        }
        public static bool operator >(int value, Stat a)
        {
            if (a == null)
            {
                return false;
            }
            return value > a.Value;
        }
        public static bool operator <(Stat a, int value)
        {
            if (a == null)
            {
                return false;
            }
            return a.Value < value;
        }
        public static bool operator <(int value, Stat a)
        {
            if (a == null)
            {
                return false;
            }
            return value < a.Value;
        }

        public static float operator +(Stat a, float value)
        {
            if (a == null)
            {
                return value;
            }
            return a.Value + value;
        }
        public static float operator +(float value, Stat a)
        {
            return a + value;
        }
        public static float operator -(Stat a, float value)
        {
            if (a == null)
            {
                return value;
            }
            return a.Value - value;
        }
        public static float operator -(float value, Stat a)
        {
            return a - value;
        }
        public static float operator *(Stat a, float value)
        {
            if (a == null)
            {
                return value;
            }
            return a.Value * value;
        }
        public static float operator *(float value, Stat a)
        {
            return a * value;
        }
        public static float operator /(Stat a, float value)
        {
            if (a == null)
            {
                return value;
            }
            return a.Value / value;
        }
        public static float operator /(float value, Stat a)
        {
            return a / value;
        }
        public static bool operator >(Stat a, float value)
        {
            if (a == null)
            {
                return false;
            }
            return a.Value > value;
        }
        public static bool operator >(float value, Stat a)
        {
            if (a == null)
            {
                return false;
            }
            return value > a.Value;
        }
        public static bool operator <(Stat a, float value)
        {
            if (a == null)
            {
                return false;
            }
            return a.Value < value;
        }
        public static bool operator <(float value, Stat a)
        {
            if (a == null)
            {
                return false;
            }
            return value < a.Value;
        }

        public static bool operator >=(Stat a, int value)
        {
            if (a == null)
            {
                return false;
            }
            return a.Value >= value;
        }
        public static bool operator >=(int value, Stat a)
        {
            if (a == null)
            {
                return false;
            }
            return value >= a.Value;
        }
        public static bool operator <=(Stat a, int value)
        {
            if (a == null)
            {
                return false;
            }
            return a.Value <= value;
        }
        public static bool operator <=(int value, Stat a)
        {
            if (a == null)
            {
                return false;
            }
            return value <= a.Value;
        }

        public static bool operator ==(Stat a, int value)
        {
            if (a == null)
            {
                return false;
            }
            return a.Value == value;
        }
        public static bool operator ==(int value, Stat a)
        {
            if (a == null)
            {
                return false;
            }
            return value == a.Value;
        }

        public static bool operator !=(Stat a, int value)
        {
            if (a == null)
            {
                return true;
            }
            return a.Value != value;
        }
        public static bool operator !=(int value, Stat a)
        {
            if (a == null)
            {
                return true;
            }
            return value != a.Value;
        }
        public static int operator +(Stat a, Stat b)
        {
            if (a != null && b != null)
            {
                return a.Value + b.Value;
            }
            return 0;
        }
        public static int operator -(Stat a, Stat b)
        {
            if (a != null && b != null)
            {
                return a.Value - b.Value;
            }
            return 0;
        }
        public static int operator *(Stat a, Stat b)
        {
            if (a != null && b != null)
            {
                return a.Value * b.Value;
            }
            return 0;
        }
        public static int operator /(Stat a, Stat b)
        {
            if (a != null && b != null)
            {
                return a.Value / b.Value;
            }
            return 0;
        }
        public static bool operator >(Stat a, Stat b)
        {
            if (a == null)
            {
                return false;
            }
            if (b == null)
            {
                return true;
            }
            return a.Value > b.Value;
        }
        public static bool operator <(Stat a, Stat b)
        {
            if (a == null)
            {
                return false;
            }
            if (b == null)
            {
                return true;
            }
            return a.Value < b.Value;
        }
        public static bool operator >=(Stat a, Stat b)
        {
            if (a == null)
            {
                return false;
            }
            if (b == null)
            {
                return true;
            }
            return a.Value >= b.Value;
        }
        public static bool operator <=(Stat a, Stat b)
        {
            if (a == null)
            {
                return false;
            }
            if (b == null)
            {
                return true;
            }
            return a.Value <= b.Value;
        }
    }
}