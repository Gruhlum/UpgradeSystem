using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    public interface IHasUpgrade
    {
        public bool IsValidUpgrade(Rarity rarity);
        public Upgrade GetUpgrade(Rarity rarity);
    }
}