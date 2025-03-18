using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [CreateAssetMenu(menuName = "HexTecGames/UpgradeSystem/UnitStatsData")]
    public class UnitStatsData : ScriptableObject
    {
        public StatData MaxHP;
        public StatData Damage;
        public StatData CritChance;
        public StatData CritMulti;
        public StatData AttackSpeed;
    }
}