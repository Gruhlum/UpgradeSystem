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

        public override bool IsValid(Stat stat, Rarity rarity, List<Stat> allStats)
        {
            Stat otherStat = allStats.Find(statType);
            return EquateSymbol(symbol, otherStat.Value, compareValue);
        }

        protected override void CopyTo(Condition condition)
        {
            if (condition is StatCondition statCondition)
            {
                statCondition.statType = statType;
                statCondition.symbol = symbol;
                statCondition.compareValue = compareValue;
            }
        }

        protected override Condition InstantiateCondition()
        {
            return new StatCondition();
        }
    }
}