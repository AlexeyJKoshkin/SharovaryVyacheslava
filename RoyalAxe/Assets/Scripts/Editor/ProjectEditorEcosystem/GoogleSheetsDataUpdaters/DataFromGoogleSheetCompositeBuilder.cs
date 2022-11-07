using System;
using System.Collections.Generic;
using System.Reflection;
using Core.EditorCore.Parser;
using Core.Parser;
using UnityEngine;

namespace ProjectEditorEcosystem.GoogleSheetsDataUpdaters
{
    public class DataFromGoogleSheetCompositeBuilder<T>
    {
        private readonly Dictionary<string, IGameDataParser> _fieldCustomParsers = new Dictionary<string, IGameDataParser>();
        private readonly Dictionary<string, FieldInfo> _fieldNameMap = new Dictionary<string, FieldInfo>();

        public DataFromGoogleSheetCompositeBuilder()
        {
            LoadTypeFields();
        }

        public object Load(object data, IEnumerable<GoogleSheetGameData> pages)
        {
            foreach (var page in pages)
            {
                if (_fieldCustomParsers.TryGetValue(page.PageName, out var parser) && _fieldNameMap.TryGetValue(page.PageName, out var fieldInfo))
                {
                    var cells      = page.Cells[0];
                    var fieldValue = fieldInfo.GetValue(data);
                    parser.UpdateObject(cells, fieldValue);
                    fieldInfo.SetValue(data, fieldValue);
                }
            }

            return data;
        }

        private void LoadTypeFields()
        {
            _fieldCustomParsers.Clear();
            LoadField(typeof(T));
        }

        private void LoadField(Type type)
        {
            foreach (var fieldInfo in type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public))
            {
                AddPageField(fieldInfo);
            }

            var baseType = type.BaseType;
            if (baseType != null && baseType != typeof(object))
            {
                LoadField(baseType);
            }
        }

        private void AddPageField(FieldInfo fieldInfo)
        {
            var attribute = fieldInfo.GetCustomAttribute<GoogleSheetPageNameAttribute>();
            if (attribute == null) return;
            string pageName = attribute.SameAsFieldName ? fieldInfo.Name : attribute.PageName;


            if (_fieldCustomParsers.ContainsKey(pageName))
            {
                Debug.LogError($"{typeof(T).Name} duplicate page at {fieldInfo.Name}");
                return;
            }

            var parserType = typeof(GenericParser<>).MakeGenericType(fieldInfo.FieldType);
            _fieldCustomParsers.Add(pageName, Activator.CreateInstance(parserType) as IGameDataParser);
            _fieldNameMap.Add(pageName, fieldInfo);
        }
    }
}