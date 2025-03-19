using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [CreateAssetMenu(menuName = "HexTecGames/UpgradeSystem/RarityType")]
    public class Rarity : ScriptableObject
    {
        [SerializeField] private RarityCollection rarityCollection = default;


        public Rarity GetRarity(int change)
        {
            return rarityCollection.GetRarity(this, change);
        }
    }
}