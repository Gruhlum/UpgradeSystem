using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [CreateAssetMenu(menuName = "HexTecGames/Default Project Universal 2D/RarityCollection")]
    public class RarityCollection : ScriptableObject
    {
        [SerializeField] private List<Rarity> rarities = default;

        public Rarity GetRarity(Rarity rarity, int change)
        {
            int index = rarities.IndexOf(rarity);
            index += change;
            index = Mathf.Clamp(index, 0, rarities.Count - 1);
            return rarities[index];
        }
    }
}