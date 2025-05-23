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
        [SerializeField] private string shortName = default;
        [SerializeField] private bool ignore = default;
        [SerializeField] private List<StatTag> tags = default;


        public string Formatting
        {
            get
            {
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

        public string ShortName
        {
            get
            {
                return this.shortName;
            }
            set
            {
                this.shortName = value;
            }
        }

        public Sprite Icon
        {
            get
            {
                return this.icon;
            }
            set
            {
                this.icon = value;
            }
        }

        public bool Ignore
        {
            get
            {
                return this.ignore;
            }
            set
            {
                this.ignore = value;
            }
        }

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(ShortName))
            {
                ShortName = name;
            }
        }
    }
}