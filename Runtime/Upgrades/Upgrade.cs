using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public abstract class Upgrade
    {
        public Rarity rarity;
        public int tickets;

        protected Upgrade(Rarity rarity, int tickets)
        {
            this.rarity = rarity;
            this.tickets = tickets;
        }

        public abstract string GetDescription();

        public abstract string GetExtraDescription();

        public abstract void Apply();
    }
}