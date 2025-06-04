using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public class DescriptionText
    {
        [TextArea] public string description;
        public List<string> linkTexts = new List<string>();
    }
}