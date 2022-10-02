using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GameKit.Editor;
using UnityEngine;

namespace Core.Parser
{
    public interface IComplexTypeParser
    {
        char SplitChar { get; }
        object Parse(string cellValue);
    }

    public interface ICellValue
    {
        string Value { get; }
        string ColumnName { get; }
    }

    internal static class ComplexTypeParserProvider
    {
        private static readonly Dictionary<Type, Type> _allCustomObjectParsers = new Dictionary<Type, Type>();

        static ComplexTypeParserProvider()
        {
            foreach (var parserType in ReflectionHelper.GetAllTypesInSolutionWithInterface<IComplexTypeParser>())
            {
                var attr = parserType.GetCustomAttribute<ComplexTypeParserAttribute>();
                if (attr == null)
                {
                    continue;
                }

                _allCustomObjectParsers.Add(attr.ParseableType, parserType);
            }
        }

        public static IComplexTypeParser TryCreateCustomParser(Type type)
        {
            if (_allCustomObjectParsers.TryGetValue(type, out var parserType))
            {
                return Activator.CreateInstance(parserType) as IComplexTypeParser;
            }

            return null;
        }
    }

    public class GenericParser<T> : IGameDataParser
    {
        private readonly Dictionary<Type, IComplexTypeParser> _complexTypeParsers = new Dictionary<Type, IComplexTypeParser>();

        private readonly Dictionary<string, FieldInfo> _fieldNameMap = new Dictionary<string, FieldInfo>();

        private readonly Dictionary<Type, MethodInfo> _simpleTypeParserMethod = new Dictionary<Type, MethodInfo>();

        public GenericParser()
        {
            LoadTypeFields();
            LoadParserMethods(typeof(CommonTypeParser));
            CheckHasAllTypes();
        }

        public void UpdateObject(List<ICellValue> cells, object result)
        {
            foreach (var cell in cells.Where(e => !string.IsNullOrEmpty(e.Value)))
                if (_fieldNameMap.TryGetValue(cell.ColumnName, out var fieldInfo))
                {
                    var value = GetValue(fieldInfo.FieldType, cell.Value);
                    fieldInfo.SetValue(result, value);
                }
        }

        public object GetValue(string columnName, string cellValue)
        {
            return _fieldNameMap.TryGetValue(columnName, out var fieldInfo) ? GetValue(fieldInfo.FieldType, cellValue) : null;
        }

        public bool UpdateObjectValue(object data, string columnName, string cellValue)
        {
            if (_fieldNameMap.TryGetValue(columnName, out var fieldInfo))
            {
                var value = GetValue(fieldInfo.FieldType, cellValue);
                fieldInfo.SetValue(data, value);
                return false;
            }

            return true;
        }

        private object GetValue(Type fieldType, string cellValue)
        {
            if (fieldType.IsArray)
            {
                var elementType = fieldType.GetElementType();

                if (elementType.IsEnum)
                {
                    return CommonTypeParser.ParseEnumArray(fieldType, cellValue);
                }

                return ParseArray(fieldType, elementType, cellValue);
            }

            if (IsList(fieldType))
            {
                var elementType = fieldType.GetGenericArguments()[0];
                return ParseList(elementType, cellValue);
            }

            if (fieldType.IsEnum)
            {
                return CommonTypeParser.ParseEnum(fieldType, cellValue);
            }

            if (_simpleTypeParserMethod.TryGetValue(fieldType, out var parser))
            {
                return parser.Invoke(null, new object[] {cellValue});
            }

            if (_complexTypeParsers.TryGetValue(fieldType, out var customObjectParser))
            {
                return customObjectParser.Parse(cellValue);
            }

            return null;
        }

        private object ParseArray(Type fieldType, Type arraytype, string cellValue)
        {
            Func<string, object> parser    = null;
            char                 splitChar = CommonTypeParser.SPLIT_ARRAY_SYMBOL;

            if (_simpleTypeParserMethod.TryGetValue(arraytype, out var methodInfo))
            {
                parser = s => methodInfo.Invoke(null, new object[] {s});
            }
            else if (_complexTypeParsers.TryGetValue(arraytype, out var customParser))
            {
                parser    = customParser.Parse;
                splitChar = customParser.SplitChar;
            }

            if (parser != null)
            {
                return CommonTypeParser.ParseArray(fieldType, cellValue, parser, splitChar);
            }

            return null;
        }

        private object ParseList(Type elementType, string cellValue)
        {
            var list = (IList) Activator.CreateInstance(typeof(List<>).MakeGenericType(elementType));

            foreach (var valueString in cellValue.Split(CommonTypeParser.SPLIT_ARRAY_SYMBOL))
            {
                var value = GetValue(elementType, valueString);
                list.Add(value);
            }

            return list;
        }

        public bool CanRead(string cellColumnId)
        {
            return _fieldNameMap.ContainsKey(cellColumnId);
        }

        private static bool IsList(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>);
        }

        #region Initing

        private void CheckHasAllTypes()
        {
            foreach (var fieldType in _fieldNameMap.Values.Select(o => o.FieldType))
            {
                if (CanParse(fieldType))
                {
                    continue;
                }

                Debug.LogError($"Not found parser for {fieldType.Name}");
            }
        }

        private bool CanParse(Type type)
        {
            if (type.IsEnum)
            {
                return true;
            }

            if (type.IsArray)
            {
                return CanParse(type.GetElementType());
            }

            if (IsList(type))
            {
                return CanParse(type.GetGenericArguments()[0]);
            }

            if (_simpleTypeParserMethod.ContainsKey(type))
            {
                return true;
            }

            var customParser = ComplexTypeParserProvider.TryCreateCustomParser(type);

            if (customParser == default)
            {
                return false;
            }

            _complexTypeParsers[type] = customParser;
            return true;
        }

        private void LoadTypeFields()
        {
            _fieldNameMap.Clear();
            LoadField(typeof(T));
        }

        private void LoadField(Type type)
        {
            foreach (var fieldInfo in type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public))
            {
                var attribute = fieldInfo.GetCustomAttribute<ColumnNameAttribute>();
                if (attribute == null)
                {
                    continue;
                }

                if (_fieldNameMap.ContainsKey(attribute.ColumnName))
                {
                    Debug.LogError($"{typeof(T).Name} duplicate RowName at {fieldInfo.Name}. Origin {_fieldNameMap[attribute.ColumnName].Name}");
                    continue;
                }

                _fieldNameMap.Add(attribute.ColumnName, fieldInfo);
            }

            var baseType = type.BaseType;
            if (baseType != null && baseType != typeof(object))
            {
                LoadField(baseType);
            }
        }

        private void LoadParserMethods(Type parseMethodsLibrary)
        {
            // библиотеки для парсингда должны быть статичными классами
            bool isStaticClass = parseMethodsLibrary.IsSealed && parseMethodsLibrary.IsAbstract;
            if (!isStaticClass)
            {
                return;
            }

            foreach (var methodInfo in parseMethodsLibrary.GetMethods())
            {
                // достаем только методы который принимают string
                var parameters = methodInfo.GetParameters();
                if (parameters.Length != 1 || parameters[0].ParameterType != typeof(string))
                {
                    continue;
                }

                _simpleTypeParserMethod.Add(methodInfo.ReturnType, methodInfo);
            }
        }

        #endregion
    }
}