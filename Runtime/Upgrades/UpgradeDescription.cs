using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexTecGames.UpgradeSystem
{
    [System.Serializable]
    public class UpgradeDescription
    {
        public List<string> linkResults = new List<string>();
        public string description;
    }
}