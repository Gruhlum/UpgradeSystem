using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{    
    public interface IUpgradeableStatCollection
    {
        public StatUpgradeData StatUpgradeData
        {
            get;
            set;
        }

        public int GetTotalTickets();

        public List<Stat> GetStats();
    }
}