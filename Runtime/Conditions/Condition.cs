using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public abstract class Condition
    {
       

        public abstract bool IsValid(Stat stat, Rarity rarity, List<Stat> allStats);

        protected bool EquateSymbol(EquationSymbol symbol, int value1, int value2)
        {
            switch (symbol)
            {
                case EquationSymbol.Equal:
                    return value1 == value2;
                case EquationSymbol.Greater:
                    return value1 > value2;
                case EquationSymbol.Less:
                    return value1 < value2;
                default:
                    Debug.Log("Invalid Symbol!");
                    return false;
            }
        }

        public Condition CreateCopy()
        {
            Condition clone = InstantiateCondition();
            CopyTo(clone);
            return clone;
        }
        protected abstract void CopyTo(Condition condition);

        protected abstract Condition InstantiateCondition();
    }
}