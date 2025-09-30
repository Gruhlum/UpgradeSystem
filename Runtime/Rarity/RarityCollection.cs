using System.Collections.Generic;
using HexTecGames.Basics;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [CreateAssetMenu(menuName = "HexTecGames/UpgradeSystem/RarityCollection")]
    public class RarityCollection : ScriptableObjectCollection<Rarity>
    {
        public Rarity GetRarity(Rarity rarity, int change)
        {
            int index = Items.IndexOf(rarity);
            index += change;
            index = Mathf.Clamp(index, 0, Items.Count - 1);
            return Items[index];
        }
        public Rarity GetRarityByIndex(int index)
        {
            if (index < 0 || index >= Items.Count)
            {
                return null;
            }
            return Items[index];
        }
    }
}