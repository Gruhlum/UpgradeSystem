using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [CreateAssetMenu(menuName = "HexTecGames/UpgradeSystem/RarityType")]
    public class Rarity : TagType
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

        public float GetMultiplier(Rarity other)
        {
            int difference = this.GetIndex() - other.GetIndex();
            return GetMultiplier(difference);
        }
        public float GetMultiplier()
        {
            return GetMultiplier(GetIndex());
        }
        private float GetMultiplier(int value)
        {
            return Mathf.Pow(2, value);
        }
        public static bool operator >(Rarity r1, Rarity r2)
        {
            return r1.GetIndex() > r2.GetIndex();
        }

        public static bool operator <(Rarity r1, Rarity r2)
        {
            return r1.GetIndex() < r2.GetIndex();
        }

        public static bool operator >=(Rarity r1, Rarity r2)
        {
            return r1.GetIndex() >= r2.GetIndex();
        }

        public static bool operator <=(Rarity r1, Rarity r2)
        {
            return r1.GetIndex() <= r2.GetIndex();
        }
        public static int operator -(Rarity r1, Rarity r2)
        {
            return r1.GetIndex() - r2.GetIndex();
        }
        public static int operator +(Rarity r1, Rarity r2)
        {
            return r1.GetIndex() + r2.GetIndex();
        }
    }
}