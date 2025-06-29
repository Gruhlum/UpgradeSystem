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

        public GroupCondition(List<Condition> conditions)
        {
            this.conditions = conditions;
        }

        public override bool IsValid(Stat stat, Rarity rarity)
        {
            if (conditionType == ConditionType.Any)
            {
                foreach (var condition in conditions)
                {
                    if (condition.IsValid(stat, rarity))
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
                    if (!condition.IsValid(stat, rarity))
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public override Condition Create(List<Stat> allStats)
        {
            List<Condition> conditionCopies = new List<Condition>();
            foreach (var condition in conditions)
            {
                conditionCopies.Add(condition.Create(allStats));
            }
            return new GroupCondition(conditionCopies);
        }
    }
}