using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace HexTecGames.Editor
{
    [CustomEditor(typeof(UpgradeList))]
    public class UpgradeListEditor : UnityEditor.Editor
    {
        UpgradeList upgradeList;


        private void OnEnable()
        {
            upgradeList = (UpgradeList)target;
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField($"Stats without Upgrades: {upgradeList.TotalStats - upgradeList.TotalUpgrades}");
            base.OnInspectorGUI();
        }
    }
}