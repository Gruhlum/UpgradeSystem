using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [CreateAssetMenu(menuName = "HexTecGames/UpgradeSystem/RarityType")]
    public class Rarity : ScriptableObject
    {
        [SerializeField] private RarityCollection rarityCollection = default;
        public Sprite Icon
        {
            get
            {
                return icon;
            }
            private set
            {
                icon = value;
            }
        }
        [SerializeField] private Sprite icon;

        public Color Color
        {
            get
            {
                return color;
            }
            private set
            {
                color = value;
            }
        }
        [SerializeField] private Color color = Color.white;


        public int GetIndex()
        {
            return rarityCollection.GetIndex(this);
        }
        public Rarity GetRarityByIndex(int index)
        {
            return rarityCollection.GetRarityByIndex(index);
        }
        public Rarity GetRarity(int change)
        {
            return rarityCollection.GetRarity(this, change);
        }
    }
}