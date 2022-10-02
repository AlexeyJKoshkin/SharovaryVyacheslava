using System;
using System.Text.RegularExpressions;
using Core;
using GameKit.Editor;
using UnityEditor;
using UnityEngine;

namespace CodeGenerator.EnumGenerator
{
    public class EnumGeneratorLayout
    {
        private readonly GUIContent _helpBtnContent;

        private readonly GUIStyle _labelRichText;

        //можно ли удалять значение енума
        private bool _canRemove;

        /// <summary>
        ///     Ссылка на текущую доп информацию по выбранному типу енама
        /// </summary>
        private IAdditionalEnumInfo _currentAdditional;

        private Type _currentEnumType;

        /// <summary>
        ///     Слой отрисовки значения енума
        /// </summary>
        private EditableEnumValuesCandidate _enumValuesLayout;

        private Vector2 _scrollPostion;

        private string _valueCandidate, _comment, _errorText;
        public string CANDIDATE_TEXT = "Enter new value:";

        public float Width = 300;

        public EnumGeneratorLayout(string valueCandidate = null)
        {
            SetDefaultName(valueCandidate);
            //  _enumValuesLayout = new EditableEnumValuesCandidate();

            var blank = EditorGUIUtility.GetBuiltinSkin(EditorSkin.Inspector).FindStyle("label");
            _labelRichText = new GUIStyle(blank) {richText = true, wordWrap = true};

            Texture2D icon = EditorGUIUtility.FindTexture("console.infoicon");

            _helpBtnContent = new GUIContent
            {
                image   = icon,
                tooltip = "Перейти в доки"
            };
        }

        public EnumGeneratorLayout(Type EnumType, IAdditionalEnumInfo additional) : this()
        {
            SelectNewTypeHandler(EnumType, additional);
        }

        public void SetDefaultName(string valueCandidate)
        {
            if (string.IsNullOrEmpty(valueCandidate))
            {
                _valueCandidate = CANDIDATE_TEXT;
            }
            else
            {
                CANDIDATE_TEXT  = valueCandidate;
                _valueCandidate = valueCandidate;
            }
        }

        public void SelectNewTypeHandler(Type enumType, IAdditionalEnumInfo additional)
        {
            _canRemove         = enumType.GetCustomAttribute<CanGenerateAttribute>(true).CanRemoveValue;
            _currentEnumType   = enumType;
            _currentAdditional = additional;
            var parser = new OldEnumFileParser(enumType);
            _enumValuesLayout = new EditableEnumValuesCandidate(parser);
            //_enumValuesLayout.InitWithValues(parser);
        }

        private void EnumParamsGUI()
        {
            using (new GUILayout.HorizontalScope())
            {
                EditorGUILayout.LabelField("New Value", GUILayout.Width(80));
                EditorGUI.BeginChangeCheck();
                _valueCandidate = EditorGUILayout.TextField(_valueCandidate, GUILayout.Width(Width));
                if (EditorGUI.EndChangeCheck())
                {
                    _errorText = null;
                }
            }

            using (new GUILayout.HorizontalScope())
            {
                EditorGUILayout.LabelField("Description", GUILayout.Width(80));
                _comment = EditorGUILayout.TextField(_comment, GUILayout.Width(Width));
            }
        }

        public bool DoDownArea()
        {
            var wasPresed = false;
            if (!string.IsNullOrEmpty(_errorText))
            {
                EditorGUILayout.HelpBox(_errorText, MessageType.Error);
            }
            else
            {
                EditorGUI.BeginDisabledGroup(_currentEnumType == null);
                var temp = GUI.color;
                GUI.color = Color.yellow;
                if (GUILayout.Button("Генерция кода перечисления"))
                {
                    DoGenerate();
                    wasPresed = true;
                }

                EditorGUI.EndDisabledGroup();
                GUI.color = temp;
            }

            return wasPresed;
        }

        public void DoAddNewValueArea()
        {
            using (new GUILayout.HorizontalScope("Box"))
            {
                using (new GUILayout.VerticalScope(GUILayout.ExpandWidth(false)))
                {
                    EnumParamsGUI();

                    _currentAdditional?.DoGUI();
                }

                if (GUILayout.Button("Add", GUILayout.Height(35)))
                {
                    AddEnumValueBtn(_currentAdditional);
                }

                if (GUILayout.Button(_helpBtnContent, GUILayout.Height(35), GUILayout.Width(35)))
                {
                    Application.OpenURL(
                                        "https://docs.google.com/document/d/1aW4K4wvECqFWEzM-6y6PjaeujprGFpQfb4oydl6WJT4/edit");
                }
            }
        }

        public void DoEditingArea()
        {
            using (new GUILayout.VerticalScope("Box"))
            {
                _scrollPostion = EditorGUILayout.BeginScrollView(_scrollPostion, false, false, GUIStyle.none,
                                                                 GUI.skin.verticalScrollbar, GUI.skin.scrollView);

                if (_enumValuesLayout != null)
                {
                    for (var i = 0; i < _enumValuesLayout.Count; i++)
                    {
                        var meta = _enumValuesLayout.GetEnumMeta(i);
                        if (CheckNeedShowEnumMeta(meta))
                        {
                            DrawAttr(meta.Attribute);
                            using (new GUILayout.HorizontalScope())
                            {
                                DrawValue(meta);
                                if (_canRemove)
                                {
                                    if (GUILayout.Button("Remove", GUILayout.Width(60)))
                                    {
                                        _enumValuesLayout.RemoveAt(i);
                                    }
                                }
                            }
                        }
                    }
                }

                EditorGUILayout.EndScrollView();
            }
        }

        private bool CheckNeedShowEnumMeta(IEnumValueMeta meta)
        {
            return _currentAdditional == null || _currentAdditional.CanShow(meta);
        }

        private void DrawValue(IEnumValueMeta meta)
        {
            using (new GUILayout.HorizontalScope())
            {
                EditorGUILayout.LabelField($"{meta.IntValue} = ", GUILayout.Width(35));
                meta.EnumName = EditorGUILayout.TextField(meta.EnumName);
            }
        }

        private void DrawAttr(string attrString)
        {
            if (!string.IsNullOrEmpty(attrString))
            {
                var attrBlank = $"<color=green><b>{attrString.Replace("\t", null)}</b></color>";
                EditorGUILayout.LabelField(attrBlank, _labelRichText);
            }
        }

        public void ChildGUI()
        {
            using (new GUILayout.VerticalScope("Box"))
            {
                using (new GUILayout.HorizontalScope())
                {
                    using (new GUILayout.VerticalScope())
                    {
                        EnumParamsGUI();
                        _currentAdditional?.DoGUI();
                    }

                    if (GUILayout.Button("Add", GUILayout.Height(35)))
                    {
                        AddEnumValueBtn(_currentAdditional);
                    }

                    if (GUILayout.Button(_helpBtnContent, GUILayout.Height(35), GUILayout.Width(35)))
                    {
                        Application.OpenURL(
                                            "https://docs.google.com/document/d/1aW4K4wvECqFWEzM-6y6PjaeujprGFpQfb4oydl6WJT4/edit");
                    }
                }
            }

            DoEditingArea();
            DoDownArea();
        }

        private void AddEnumValueBtn(IAdditionalEnumInfo additional)
        {
            var candidateTex = additional == null ? _valueCandidate : additional.GetEnumName(_valueCandidate);
            _errorText = Check(candidateTex, _comment);
            if (string.IsNullOrEmpty(_errorText))
            {
                var attr     = additional?.GetAttributeString();
                var intValue = additional?.GetIntValue();
                _enumValuesLayout.AddCandidate(candidateTex, _comment, attr, intValue);
                _valueCandidate = CANDIDATE_TEXT;
                _comment        = null;
            }
        }

        public void DoGenerate()
        {
            ICodeGenerator generator = new EnumGenerator(_enumValuesLayout);
            GeneratorUtils.WriteCode(_enumValuesLayout.NameType, generator);
        }

        public string Check(string valueCandidate, string comment)
        {
            if (string.IsNullOrEmpty(valueCandidate))
            {
                return "Value is NULL";
            }

            if (valueCandidate == CANDIDATE_TEXT)
            {
                return "Imposible add Default Text";
            }

            if (valueCandidate.Length < 2)
            {
                return "too short name";
            }

            if (!Regex.IsMatch(valueCandidate, @"^[a-zA-Z_]+$"))
            {
                return "Invalid values";
            }

            if (!_enumValuesLayout.CheckCandidate(valueCandidate))
            {
                return "Duplicate values";
            }

            return null;
        }
    }
}