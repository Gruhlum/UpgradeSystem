using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public class Upgrade
    {
        public Stat stat;
        public int increase;

        public Upgrade(Stat stat, int increase)
        {
            this.stat = stat;
            this.increase = increase;
        }

        public void Apply()
        {
            stat.Value += increase;
            Debug.Log($"Increased {stat.StatType} by {increase} ({stat.Value})");
        }

        public string GetDescription()
        {
            return stat.GetUpgradeDescription();
        }
    }
}