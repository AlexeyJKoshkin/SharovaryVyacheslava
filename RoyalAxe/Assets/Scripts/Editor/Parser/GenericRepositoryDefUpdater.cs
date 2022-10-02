using System.Collections.Generic;
using UnityEngine;

namespace Core.Parser
{
    internal class GenericRepositoryDefUpdater<T> where T : new()
    {
        private readonly string _fieldName;
        private readonly GenericParser<T> _parser;

        public GenericRepositoryDefUpdater(string fieldName)
        {
            _fieldName = fieldName;
            _parser    = new GenericParser<T>();
        }


        private T[] GetParsingObjects(List<List<ICellValue>> values)
        {
            var result = new T[values.Count - 1];
            for (var i = 1; i < values.Count; i++)
            {
                var currentValues = values[i];
                var model         = new T();
                _parser.UpdateObject(currentValues, model);
                result[i - 1] = model;
            }

            return result;
        }

        private void CheckColumn(List<ICellValue> row) // берем все названия колонок и проверям, умеет ли текущий парсер считывать такой параметр
        {
            foreach (var cell in row)
            {
                if (_parser.CanRead(cell.ColumnName))
                {
                    continue;
                }

                Debug.LogWarning($"Unknown columnID {cell.ColumnName}");
            }
        }
    }
}