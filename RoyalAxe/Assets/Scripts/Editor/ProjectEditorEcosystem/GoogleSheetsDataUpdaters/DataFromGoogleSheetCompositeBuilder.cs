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
            foreach (var fieldInfo in Fields(type))
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
            string pageName = GetPageName(attribute, fieldInfo);

            if (_fieldCustomParsers.ContainsKey(pageName))
            {
                Debug.LogError($"{typeof(T).Name} duplicate page at {fieldInfo.Name}");
                return;
            }

            var parser = Activator.CreateInstance(typeof(CompositeGenericParser)) as CompositeGenericParser;
            parser.Bind(fieldInfo.FieldType);
            
            
            _fieldCustomParsers.Add(pageName,parser);
            _fieldNameMap.Add(pageName, fieldInfo);
        }

       

        FieldInfo[] Fields(Type type)
        {
            return type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
        }

        private string GetPageName(GoogleSheetPageNameAttribute attribute, FieldInfo fieldInfo)
        {
            switch (attribute.FieldName)
            {
                case GoogleSheetPageNameAttribute.FieldNameType.SameAsFieldName:
                    return fieldInfo.Name;
                case GoogleSheetPageNameAttribute.FieldNameType.Custom:
                    return attribute.PageName;
                case GoogleSheetPageNameAttribute.FieldNameType.SameAsTypeName:
                    return fieldInfo.FieldType.Name;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}