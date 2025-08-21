using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{    
    public abstract class UpgradeableStatCollection<T> : CloneableStatCollection<T>, IUpgradeableStatCollection where T : StatCollection
    {
        public StatUpgradeData StatUpgradeData
        {
            get
            {
                return statUpgradeData;
            }
            set
            {
                statUpgradeData = value;
            }
        }
        [SerializeField] private StatUpgradeData statUpgradeData;

    }
}