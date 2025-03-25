using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public class RarityCondition : Condition
    {
        [SerializeField] private Rarity rarity = default;
        [SerializeField] private EquationSymbol symbol = default;
        public override bool IsValid(Stat stat, Rarity rarity, List<Stat> allStats)
        {
            return EquateSymbol(symbol, this.rarity.GetIndex(), rarity.GetIndex());
        }

        protected override void CopyTo(Condition condition)
        {
            if (condition is RarityCondition rarityCondition)
            {
                rarityCondition.rarity = this.rarity;
                rarityCondition.symbol = this.symbol;
            }
        }

        protected override Condition InstantiateCondition()
        {
            return new RarityCondition();
        }
    }
}