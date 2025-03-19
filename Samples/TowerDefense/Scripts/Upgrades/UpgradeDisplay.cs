using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics.UI;
using TMPro;
using UnityEngine;

namespace HexTecGames.UpgradeSystem.TowerDefense
{
    public class UpgradeDisplay : Display<UpgradeDisplay, Upgrade>
    {
        [SerializeField] private TMP_Text descriptionGUI = default;

        protected override void DrawItem(Upgrade item)
        {
            descriptionGUI.text = item.GetDescription();
        }
    }
}