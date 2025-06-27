using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public class StatCondition : Condition
    {
        [SerializeField] private StatType statType = default;
        [SerializeField] private EquationSymbol symbol = default;
        [SerializeField] private int compareValue = default;

        private Stat linkedStat;


        public StatCondition(StatType statType, EquationSymbol symbol, int compareValue, Stat linkedStat)
        {
            this.statType = statType;
            this.symbol = symbol;
            this.compareValue = compareValue;
            this.linkedStat = linkedStat;
        }

        public override Condition Create(Dictionary<StatType, Stat> allStats)
        {
            Stat linkedStat = allStats.GetValueOrDefault(statType);
            return new StatCondition(statType, symbol, compareValue, linkedStat);
        }

        public override bool IsValid(Stat stat, Rarity rarity)
        {
            return EquateSymbol(symbol, linkedStat.Value, compareValue);
        }
    }
}