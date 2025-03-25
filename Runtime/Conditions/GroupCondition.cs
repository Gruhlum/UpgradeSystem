using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public class GroupCondition : Condition
    {
        [SerializeField] private ConditionType conditionType = default;
        [SubclassSelector, SerializeReference] private List<Condition> conditions = default;

        public override bool IsValid(Stat stat, Rarity rarity, List<Stat> allStats)
        {
            if (conditionType == ConditionType.Any)
            {
                foreach (var condition in conditions)
                {
                    if (condition.IsValid(stat, rarity, allStats))
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                foreach (var condition in conditions)
                {
                    if (!condition.IsValid(stat, rarity, allStats))
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        protected override void CopyTo(Condition condition)
        {
            if (condition is GroupCondition groupCondition)
            {
                groupCondition.conditionType = conditionType;
                groupCondition.conditions = CopyConditions();
            }
        }

        private List<Condition> CopyConditions()
        {
            List<Condition> conditions = new List<Condition>();
            foreach (var condition in conditions)
            {
                conditions.Add(condition.CreateCopy());
            }
            return conditions;
        }

        protected override Condition InstantiateCondition()
        {
            return new GroupCondition();
        }
    }
}