using System.Collections;
using System.Collections.Generic;
using HexTecGames.Basics.Editor;
using UnityEditor;
using UnityEngine;

namespace HexTecGames.UpgradeSystem.Editor
{
    [CustomPropertyDrawer(typeof(Stat), true)]
    public class StatDrawer : SingleLineDrawer
    {
        protected override string PropertyName
        {
            get
            {
                return "statType";
            }
        }
    }
}