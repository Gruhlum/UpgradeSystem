using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public struct EfficiencyValue
    {
        public TagType type;
        public float value;
        public int order;

        public EfficiencyValue(TagType type, float value, int order)
        {
            this.value = value;
            this.type = type;
            this.order = order;
        }

        public string GetItem()
        {
            return $"{type}: {value}";
        }
    }
}