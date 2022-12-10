using RoyalAxe.Units.Stats;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace RoyalAxe.EditorInspector
{
    public class CharacterStatValueDrawer : OdinValueDrawer<CharacterStatValue>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            var value = ValueEntry.SmartValue;

            using (new GUILayout.HorizontalScope())
            {
                value.MinValue = EditorGUILayout.FloatField(value.MinValue, GUILayout.Width(50));
                value.Value    = EditorGUILayout.Slider(value.Value, value.MinValue, value.MaxValue);
                value.MaxValue = EditorGUILayout.FloatField(value.MaxValue, GUILayout.Width(50));
            }


            ValueEntry.SmartValue = value;
        }
    }
}