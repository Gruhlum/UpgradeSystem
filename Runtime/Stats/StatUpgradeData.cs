using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{    
    [CreateAssetMenu(menuName = "HexTecGames/UpgradeSystem/StatUpgradeData")]
    public class StatUpgradeData : ScriptableObject
    {
        public List<UpgradeItem> upgradeItems;
    }
}