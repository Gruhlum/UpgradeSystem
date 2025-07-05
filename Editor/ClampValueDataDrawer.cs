using UnityEditor;
using UnityEngine;

namespace HexTecGames.UpgradeSystem.Editor
{
    [CustomPropertyDrawer(typeof(ClampValueData))]
    public class ClampValueDataDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            StatClamp statClamp = (StatClamp)property.FindPropertyRelative("clampType").intValue;
            Rect startRect = position;

            GUIContent emptyLabel = new GUIContent
            {
                text = string.Empty
            };
            position.width = (position.width / 2) + 4;

            EditorGUI.PropertyField(position, property.FindPropertyRelative("clampType"), label);

            position.x += position.width + 4;
            position.width -= 12;
            if (statClamp is StatClamp.Value)
            {
                EditorGUI.PropertyField(position, property.FindPropertyRelative("value"), emptyLabel);
            }
            else if (statClamp is StatClamp.Stat)
            {
                EditorGUI.PropertyField(position, property.FindPropertyRelative("statType"), emptyLabel);
            }

            EditorGUI.EndProperty();
        }
    }
}