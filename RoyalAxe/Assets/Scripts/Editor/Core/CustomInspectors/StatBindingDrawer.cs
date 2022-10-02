using GameKit.Editor;
using RoyalAxe.Configs;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace RoyalAxe.EditorInspector
{
    public class StatBindingDrawer : OdinValueDrawer<EntityStatConfig>
    {
        private EntityStatConfig _entityStat;
        private InspectorProperty _valueProperty;

        protected override void DrawPropertyLayout(GUIContent label)
        {
            using (new GUILayout.VerticalScope("Box"))
            {
                _entityStat.Config = EditorGUILayout.ObjectField(label, _entityStat.Config, typeof(CharacterStatTypeParameters), false) as CharacterStatTypeParameters;
                if (_entityStat.Config != null)
                {
                    bool useCustom = _entityStat.Config.DefaultValue.Equals(_entityStat.Value);
                    EditorGUIExtensions.DoButton($"Use Custom {_entityStat.Config.DefaultValue}", () =>
                                                                                                  {
                                                                                                      _entityStat.Value     = _entityStat.Config.DefaultValue;
                                                                                                      ValueEntry.SmartValue = _entityStat;
                                                                                                  });

                    _valueProperty.Draw(new GUIContent("Stat Value"));
                }
            }

            ValueEntry.SmartValue = _entityStat;
        }

        protected override void Initialize()
        {
            base.Initialize();
            _entityStat    = ValueEntry.SmartValue;
            _valueProperty = Property.FindChild(s => s.Name == "Value", false);
        }
    }
}