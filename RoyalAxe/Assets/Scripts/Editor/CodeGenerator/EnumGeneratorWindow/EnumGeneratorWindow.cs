using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using GameKit.Editor;
using UnityEditor;
using UnityEngine;

namespace CodeGenerator.EnumGenerator
{
    /// <summary>
    ///     Окно генерации енумов
    /// </summary>
    public class EnumGeneratorWindow : EditorWindow
    {
        /// <summary>
        ///     Дополнительные настройки в зависимости от типа енум
        /// </summary>
        private readonly Dictionary<Type, IAdditionalEnumInfo> _additionalInfos =
            new Dictionary<Type, IAdditionalEnumInfo>();

        private EnumWindowArea _area;
        private EnumGeneratorLayout _enumGeneratorLayout;

        /// <summary>
        ///     Тип енума для догенерации
        /// </summary>
        private SelectionGUI<Type> _enumTypeSelection;

        #region initialize

        private void OnEnable()
        {
            _enumGeneratorLayout = new EnumGeneratorLayout();

            var types = ReflectionHelper.GetAllTypeWithAttribute<Enum, CanGenerateAttribute>().Select(o => o.Key);
            _enumTypeSelection = new PopUpSelector<Type>(types) {OnSelectNewItemCallback = OnChangeSelectedEnum};

            SetSise(new Vector2(550, 600));
        }

        private void OnChangeSelectedEnum(Type obj)
        {
            IAdditionalEnumInfo additional = null;
            _additionalInfos.TryGetValue(obj, out additional);
            _enumGeneratorLayout.SelectNewTypeHandler(obj, additional);
        }

        private void SetSise(Vector2 size)
        {
            maxSize  = size;
            minSize  = size;
            position = new Rect(position.position, size);
            _area    = new EnumWindowArea(position);
        }

        #endregion initialize

        #region GUI

        private void OnGUI()
        {
            if (EditorApplication.isCompiling)
            {
                EditorGUILayout.LabelField("PLEASE WAIT");
                return;
            }

            if (_enumGeneratorLayout == null)
            {
                OnEnable();
                Repaint();
            }

            DoWindow();
        }

        private void DoWindow()
        {
            using (new GUILayout.AreaScope(_area.DownArea))
            {
                _enumGeneratorLayout.DoDownArea();
            }

            using (new GUILayout.AreaScope(_area.AddValueSettingsArea))
            {
                using (new GUILayout.VerticalScope("Box", GUILayout.Width(_area.AddValueSettingsArea.width)))
                {
                    SelectEnumForGenerationGUI();
                    _enumGeneratorLayout.DoAddNewValueArea();
                }
            }

            using (new GUILayout.AreaScope(_area.EditingArea))
            {
                _enumGeneratorLayout.DoEditingArea();
            }
        }

        private void SelectEnumForGenerationGUI()
        {
            using (new GUILayout.HorizontalScope())
            {
                EditorGUILayout.LabelField(new GUIContent("Select Enum", "Выбрать енум для догенерации"),
                                           GUILayout.Width(80));
                _enumTypeSelection.DoSelectGUI();
            }
        }

        #endregion GUI
    }
}