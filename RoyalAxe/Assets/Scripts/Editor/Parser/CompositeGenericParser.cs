using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Parser
{
    public class CompositeGenericParser : IGameDataParser
    {
        private readonly Dictionary<Type, IGameDataParser> _typesParsers = new Dictionary<Type, IGameDataParser>();

        public CompositeGenericParser Bind<T>()
        {
            _typesParsers.Add(typeof(T), new GenericParser<T>());
            return this;
        }
        
        public CompositeGenericParser Bind(Type type)
        {
            var parser = Activator.CreateInstance(typeof(GenericParser<>).MakeGenericType(type)) as IGameDataParser;
            if (parser != null)
                _typesParsers.Add(type, parser);
            else
            {
                HLogger.LogError($"Error when create GenericParser<> with params {type.Name}");
            }
            return this;
        }

        public void UpdateObject(List<ICellValue> cells, object data)
        {
            if (data == null)
            {
                return;
            }

            if (_typesParsers.TryGetValue(data.GetType(), out var parser))
            {
                parser.UpdateObject(cells, data);
            }
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