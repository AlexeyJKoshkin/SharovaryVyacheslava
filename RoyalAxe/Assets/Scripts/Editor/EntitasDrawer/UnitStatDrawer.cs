using System;
using Entitas.VisualDebugging.Unity.Editor;
using RoyalAxe.CharacterStat;
using UnityEditor;

namespace RoyalAxe.EntitasSystems.EditorDrawers
{
    public class UnitStatDrawer : ITypeDrawer
    {
        public bool HandlesType(Type type)
        {
            return type == typeof(ModifiableStat);
        }

        public object DrawAndGetNewValue(Type memberType, string memberName, object value, object target)
        {
            var stat = value as ModifiableStat;
            EditorGUILayout.LabelField($"[{stat.MinValue}    {stat.CurrentValue}    {stat.MaxValue}]");
            return stat;
        }
    }
}