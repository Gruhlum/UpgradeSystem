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

        public RarityCondition(Rarity rarity, EquationSymbol symbol)
        {
            this.rarity = rarity;
            this.symbol = symbol;
        }

        public override Condition Create(List<Stat> allStats)
        {
            return new RarityCondition(rarity, symbol);
        }

        public override bool IsValid(Stat stat, Rarity rarity)
        {
            return EquateSymbol(symbol, this.rarity.GetIndex(), rarity.GetIndex());
        }
    }
}