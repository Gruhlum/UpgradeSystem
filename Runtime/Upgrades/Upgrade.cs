using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics;
using HexTecGames.Basics.UI;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public abstract class Upgrade : ITicket
    {      
        public Rarity rarity;

        public virtual int Tickets
        {
            get
            {
                return tickets;
            }
            set
            {
                tickets = value;
            }
        }
        private int tickets;

        protected Upgrade(Rarity rarity, int tickets)
        {
            this.rarity = rarity;
            this.Tickets = tickets;
        }
        public abstract TableText GetDescription();

        public abstract string GetExtraDescription();

        public abstract void Apply();
    }
}