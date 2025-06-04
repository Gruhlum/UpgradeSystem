using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HexTecGames.Basics;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public class Stat : IEquatable<Stat>, ITicket
    {
        public StatType StatType
        {
            get
            {
                return statType;
            }
            set
            {
                statType = value;
            }
        }
        [SerializeField] private StatType statType;

        public int FlatValue
        {
            set
            {
                flatValue = value;
                UpdateValue();
            }
            get
            {
                return flatValue;
            }
        }
        [SerializeField] private int flatValue = default;

        [SerializeField] private ClampValueData minValueData;
        [SerializeField] private ClampValueData maxValueData;

        public UpgradeInfo UpgradeInfo
        {
            get
            {
                return this.upgradeInfo;
            }
            set
            {
                this.upgradeInfo = value;
            }
        }
        [SerializeField] private UpgradeInfo upgradeInfo;

        public int Value
        {
            get
            {
                return value;
            }
            private set
            {
                this.value = value;
            }
        }
        private int value;

        public ClampValue MinValue
        {
            get
            {
                return minValue;
            }
            private set
            {
                minValue = value;
            }
        }
        private ClampValue minValue;

        public ClampValue MaxValue
        {
            get
            {
                return maxValue;
            }
            private set
            {
                maxValue = value;
            }
        }

        public int Tickets
        {
            get
            {
                return UpgradeInfo.Tickets;
            }
        }

        private ClampValue maxValue;

        private List<ValueChange> multipliers = new List<ValueChange>();


        private int increasePerLevelUp;


        public event Action<Stat, int> OnValueChanged;
        public event Action<Stat, int> OnUpgraded;


        public Stat()
        { }
        public Stat(StatType statType)
        {
            this.StatType = statType;
            UpgradeInfo = new UpgradeInfo();
            minValueData = new ClampValueData();
            maxValueData = new ClampValueData();
        }

        public Stat CreateCopy()
        {
            Stat stat = InstantiateCopy();
            CopyTo(stat);
            return stat;
        }
        protected virtual Stat InstantiateCopy()
        {
            return new Stat(StatType);
        }
        public void CopyFrom(Stat stat)
        {
            this.StatType = stat.StatType;
            this.FlatValue = stat.FlatValue;
            this.Value = stat.Value;
            this.UpgradeInfo = stat.UpgradeInfo;
            if (stat.MinValue != null) this.MinValue = stat.MinValue.CreateCopy();
            if (stat.MaxValue != null) this.MaxValue = stat.MaxValue.CreateCopy();
            this.minValueData = stat.minValueData.CreateCopy();
            this.maxValueData = stat.maxValueData.CreateCopy();
        }
        protected virtual void CopyTo(Stat stat)
        {
            stat.CopyFrom(this);
        }

        public void AddLevelUpValue(int value)
        {
            increasePerLevelUp += value;
        }

        public void LevelUp()
        {
            if (increasePerLevelUp <= 0)
            {
                return;
            }
            Debug.Log($"Increasing {statType.name} by {increasePerLevelUp}");
            FlatValue += increasePerLevelUp;
            OnUpgraded?.Invoke(this, increasePerLevelUp);
        }

        public void AddMultiplier(ValueChange multiplier)
        {
            multiplier.OnRemoved += Multiplier_OnRemoved;
            multiplier.OnValueChanged += Multiplier_OnValueChanged;
            multipliers.Add(multiplier);
            int oldValue = Value;
            UpdateValue();
        }

        private void Multiplier_OnValueChanged(ValueChange multiplier, int value)
        {
            UpdateValue();
        }

        private void Multiplier_OnRemoved(ValueChange multiplier)
        {
            multiplier.OnRemoved -= Multiplier_OnRemoved;
            multiplier.OnValueChanged -= Multiplier_OnValueChanged;
            multipliers.Remove(multiplier);
            UpdateValue();
        }

        private void UpdateValue()
        {
            Value = CalculateValue();
            OnValueChanged?.Invoke(this, Value);
        }

        private int CalculateValue()
        {
            int result = FlatValue;
            result = MultiplyFlatValue(result);
            //result = ClampValue(result);
            return result;
        }

        private int MultiplyFlatValue(int baseValue)
        {
            //Value = 20;
            //Multipliers = 50, 100, 20, 15

            if (multipliers == null || multipliers.Count == 0)
            {
                return baseValue;
            }

            float multi = 1;
            for (int i = 0; i < multipliers.Count; i++)
            {
                multi *= 1 + multipliers[i].Value / 100f; // 4.14
            }

            return Mathf.RoundToInt(baseValue * multi); // 82.8
        }

        public void Initialize(List<Stat> allStats)
        {
            MinValue = minValueData.GenerateClampValue(ClampType.Min, allStats);
            MaxValue = maxValueData.GenerateClampValue(ClampType.Max, allStats);
            UpgradeInfo = UpgradeInfo.Create(this, allStats);
            multipliers = new List<ValueChange>();
            UpdateValue();
        }

        public void ApplyData(int startValue, UpgradeInfo upgradeInfo)
        {
            this.Value = startValue;
            this.UpgradeInfo = upgradeInfo;
        }

        public bool CanBeSpecialUpgrade(Rarity rarity, float efficiency)
        {
            if (!IsValidUpgrade(rarity))
            {
                return false;
            }
            else return UpgradeInfo.CanBeSpecialUpgrade(this, rarity, efficiency);
        }

        public bool IsValidUpgrade(Rarity rarity)
        {
            if (upgradeInfo == null)
            {
                return false;
            }
            bool valid = upgradeInfo.IsValidUpgrade(this, rarity);
            return valid;
        }
        public StatUpgrade GetUpgrade(Rarity rarity, Efficiency efficiency)
        {
            return upgradeInfo.GetUpgrade(this, efficiency, rarity);
        }
        public void Upgrade(Rarity rarity, float efficiency)
        {
            int increase = upgradeInfo.ApplyUpgrade(this, rarity, efficiency);
            OnUpgraded?.Invoke(this, increase);
        }
        public override string ToString()
        {
            return $"{StatType.name}: {Value.ToString(StatType.Formatting)}";
        }

        public string GetValueString()
        {
            if (StatType.IsPercent)
            {
                return Value.ToString(StatType.Formatting);
            }
            else return Value.ToString(StatType.Formatting);
        }
        //public string GetUpgradeDescription()
        //{
        //    return $"{StatType.name}{Environment.NewLine} {Value.ToString(Formatting)} -> {(Value + upgradeValue.increase).ToString(Formatting)}";
        //}

        private void RemoveEvents(ref Stat stat)
        {
            stat.OnValueChanged -= Stat_OnValueChanged;
        }
        private void AddEvents(ref Stat stat)
        {
            stat.OnValueChanged += Stat_OnValueChanged;
        }

        private void Stat_OnValueChanged(Stat stat, int value)
        {
            this.Value = ClampValue(value);
        }

        private int ClampValue(int value)
        {
            if (MinValue != null)
            {
                value = minValue.Clamp(value);
            }
            if (MaxValue != null)
            {
                value = maxValue.Clamp(value);
            }

            return value;
        }

        public void SetMinValue(int minValue)
        {
            MinValue = new ClampValue(ClampType.Min, minValue);
        }
        public void SetMinValue(Stat minStat)
        {
            MinValue = new ClampValue(ClampType.Min, minStat);
            AddEvents(ref minStat);
        }
        public void SetMaxValue(int maxValue)
        {
            MaxValue = new ClampValue(ClampType.Max, maxValue);
        }
        public void SetMaxValue(Stat maxStat)
        {
            MaxValue = new ClampValue(ClampType.Max, maxStat);
            AddEvents(ref maxStat);
        }

        public override bool Equals(object obj)
        {
            return obj is Stat stat &&
                   EqualityComparer<StatType>.Default.Equals(this.StatType, stat.StatType) &&
                   this.Value == stat.Value &&
                   this.UpgradeInfo == stat.UpgradeInfo &&
                   EqualityComparer<ClampValue>.Default.Equals(this.MinValue, stat.MinValue) &&
                   EqualityComparer<ClampValue>.Default.Equals(this.MaxValue, stat.MaxValue);
        }
        public bool Equals(Stat other)
        {
            return other is not null &&
                   EqualityComparer<StatType>.Default.Equals(this.StatType, other.StatType) &&
                   this.Value == other.Value &&
                   this.UpgradeInfo == other.UpgradeInfo &&
                   EqualityComparer<ClampValue>.Default.Equals(this.MinValue, other.MinValue) &&
                   EqualityComparer<ClampValue>.Default.Equals(this.MaxValue, other.MaxValue);
        }
        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(this.StatType);
            hash.Add(this.Value);
            hash.Add(this.UpgradeInfo);
            hash.Add(this.MinValue);
            hash.Add(this.MaxValue);
            return hash.ToHashCode();
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

        public static bool operator >=(Stat a, float value)
        {
            if (a == null)
            {
                return false;
            }
            return a.Value >= value;
        }
        public static bool operator >=(float value, Stat a)
        {
            if (a == null)
            {
                return false;
            }
            return value >= a.Value;
        }
        public static bool operator <=(Stat a, float value)
        {
            if (a == null)
            {
                return false;
            }
            return a.Value <= value;
        }
        public static bool operator <=(float value, Stat a)
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