using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public class UpgradeValue
    {
        public Rarity rarity;
        public UpgradeType upgradeType;
        public int increase;
    }
}