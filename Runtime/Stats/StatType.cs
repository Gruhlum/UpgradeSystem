using System.Collections.Generic;
using HexTecGames.Basics;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [CreateAssetMenu(menuName = "HexTecGames/UpgradeSystem/StatType")]
    public class StatType : TagType
    {
        [SerializeField] private Sprite icon = default;

        [SerializeField] private bool isPercent = default;
        [SerializeField] private string shortName = default;
        [SerializeField] private int defaultValue = default;
        [SerializeField, TextArea] private string description = default;
        [Space]
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

        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
            }
        }

        public int DefaultValue
        {
            get
            {
                return this.defaultValue;
            }

            set
            {
                this.defaultValue = value;
            }
        }

        public string GetFormattedValue(int value)
        {
            return value.ToString(Formatting);
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