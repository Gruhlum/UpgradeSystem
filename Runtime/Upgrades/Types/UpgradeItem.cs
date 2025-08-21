using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics;
using HexTecGames.Basics.UI;
using HexTecGames.UpgradeSystem;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public class UpgradeItem
    {
        public StatType statType;

        [SubclassSelector, SerializeReference] public UpgradeEffect upgradeEffect;
    }
}