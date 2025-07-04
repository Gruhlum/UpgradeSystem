using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.UpgradeSystem.TowerDefense
{
    public class UnitController : MonoBehaviour
    {
        private List<Unit> units = new List<Unit>();
        private UnitStats unitStats;

        private void Awake()
        {
            //unitStats = new UnitStats(playerStatsData);

            //for (int i = 0; i < 5; i++)
            //{
            //    units.Add(new Se(unitStats));
            //}
        }

        [ContextMenu("Apply Upgrade")]
        public void GetUpgrade()
        {
            List<Upgrade> results = RollUpgrades(3);
            ApplyUpgrade(results.Random());
        }
        public void ApplyUpgrade(Upgrade upgrade)
        {
            upgrade.Apply();
        }

        public List<Upgrade> RollUpgrades(int totalUpgrades)
        {
            List<Upgrade> results = unitStats.GetAllValidUpgrades();
            return results.RandomUnique(totalUpgrades);
        }
    }
}