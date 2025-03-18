using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    public class UnitController : MonoBehaviour
    {
        private List<Unit> units = new List<Unit>();
        private UnitStats unitStats;
        [SerializeField] private UnitStatsData playerStatsData = default;

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
            var results = RollUpgrades(3);
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