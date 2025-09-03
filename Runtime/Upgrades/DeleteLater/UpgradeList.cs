using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HexTecGames.UpgradeSystem;
using UnityEngine;

namespace HexTecGames
{
    [CreateAssetMenu(menuName = "HexTecGames/Sandbox/UpgradeList")]
    public class UpgradeList : ScriptableObject
    {
        public StatCollectionDataBase StatCollection
        {
            get
            {
                return statCollection;
            }
        }
        [SerializeField] private StatCollectionDataBase statCollection = default;
        [SubclassSelector, SerializeReference] public List<UpgradeItem> upgradeItems;

        public int TotalStats
        {
            get
            {
                return statCollection.GetStats().Count;
            }
        }
        public int TotalUpgrades
        {
            get
            {
                return upgradeItems.Count;
            }
        }

        public void VerifyUpgrades()
        {
            if (!HasUniqueStatTypes(upgradeItems))
            {
                Debug.Log("Two Upgrades with the same StatType!");
            }
        }

        private bool HasUniqueStatTypes(List<UpgradeItem> upgradeItems)
        {
            HashSet<StatType> seenStatTypes = new HashSet<StatType>();

            foreach (var item in upgradeItems)
            {
                if (!seenStatTypes.Add(item.statType))
                {
                    // Duplicate found
                    return false;
                }
            }

            return true;
        }
    }
}