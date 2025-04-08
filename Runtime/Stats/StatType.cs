using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [CreateAssetMenu(menuName = "HexTecGames/UpgradeSystem/StatType")]
    public class StatType : ScriptableObject
    {
        [SerializeField] private Sprite icon = default;

        [SerializeField] private bool isPercent = default;


        public string Formatting
        {
            get
            {
                //return IsPercent ? "#'%'" : string.Empty;
                return IsPercent ? "#0'%'" : string.Empty;
            }
        }

        public bool IsPercent
        {
            get
            {
                return this.isPercent;
            }
            private set
            {
                this.isPercent = value;
            }
        }
    }
}