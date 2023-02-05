using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.Parser
{
    public class CompositeGenericParser : IGameDataParser
    {
        private static readonly Dictionary<Type, IGameDataParser> _typesParsers = new Dictionary<Type, IGameDataParser>();
        private static readonly HashSet<Type> _availableTypes = new HashSet<Type>();

        public CompositeGenericParser Bind<T>()
        {
            Bind(typeof(T));
            return this;
        }

        public void Bind(Type type)
        {
           if(type == null) return;
           BindRecursive(type);
        }
        
        private void BindRecursive(Type fieldInfoFieldType)
        {
            if(!CheckType(fieldInfoFieldType)) return;
            if(_availableTypes.Contains(fieldInfoFieldType)) return;

            AddType(fieldInfoFieldType);
            var fields = Fields(fieldInfoFieldType);
            for (int i = 0; i < fields.Length; i++)
            {
                var field = fields[i]; 
                BindRecursive(field.FieldType);
            }
        }

        bool CheckType(Type type)
        {
            if(type.IsPrimitive) return false;
            if(!type.IsSerializable) return false;
            var nameSpace = type.Namespace;

            if (!string.IsNullOrEmpty(nameSpace))
            {
                if(!nameSpace.Contains("RoyalAxe")) return false;
            }

            return true;
        }

        private void AddType(Type type)
        {
            _availableTypes.Add(type);
            
            var parser = Activator.CreateInstance(typeof(GenericParser<>).MakeGenericType(type)) as IGameDataParser;
            if (parser != null)
                _typesParsers.Add(type, parser);
            else
            {
                HLogger.LogError($"Error when create GenericParser<> with params {type.Name}");
            }
        }

        public object UpdateObject(List<ICellValue> cells, object data)
        {
            return RecursiveUpdate(cells, data);
        }

        private object RecursiveUpdate(List<ICellValue> cells,  object data)
        {
            if (data == null) return null;
            var typeKey = data.GetType();
            if (!CheckCanParse(typeKey)) return data;
            data = _typesParsers[typeKey].UpdateObject(cells, data);
            foreach (var field in Fields(data.GetType()))
            {
                var value = field.GetValue(data) ?? CreateDefaultValue(field.FieldType);
                value = RecursiveUpdate(cells, value);
                field.SetValue(data, value);
            }

            return data;
        }

        private object CreateDefaultValue(Type fieldFieldType)
        {
            if (fieldFieldType == typeof(string)) return null;
            
            return  Activator.CreateInstance(fieldFieldType);
        }

        private bool CheckCanParse(Type typeKey)
        {
            return CheckType(typeKey) && _typesParsers.ContainsKey(typeKey);
        }


        FieldInfo[] Fields(Type type)
        {
            return type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
        }

        public object GetValue(string columnName, string cellValue)
        {
            return _typesParsers.Values.FirstOrDefault(o => o.CanRead(columnName))?.GetValue(columnName, cellValue);
        }

        public bool UpdateObjectValue(object data, string columnName, string cellValue)
        {
            if (data == null)
            {
                return false;
            }

            return _typesParsers.TryGetValue(data.GetType(), out var parser) &&
                   parser.UpdateObjectValue(data, columnName, cellValue);
        }

        public bool CanRead(string rowName)
        {
            return _typesParsers.Values.Any(o => o.CanRead(rowName));
        }
    }
}