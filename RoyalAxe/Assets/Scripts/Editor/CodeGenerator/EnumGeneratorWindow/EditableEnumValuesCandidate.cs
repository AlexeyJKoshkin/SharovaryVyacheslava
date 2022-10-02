using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeGenerator.EnumGenerator
{
    /// <summary>
    ///     Данные по енума, с возможностью добавления, удаления значений
    /// </summary>
    public class EditableEnumValuesCandidate : IEnumMetaData
    {
        private readonly List<EnumValueMeta> _currentEnumValues = new List<EnumValueMeta>();

        private Vector2 _scrollPostion;

        public EditableEnumValuesCandidate(IEnumData enumData)
        {
            NameType        = enumData.NameType;
            IsFlag          = enumData.IsFlag;
            CanGenerateTrue = enumData.CanGenerateTrue;
            _currentEnumValues.Clear();
        }

        public EditableEnumValuesCandidate(IEnumMetaData oldInfo)
        {
            NameType        = oldInfo.NameType;
            IsFlag          = oldInfo.IsFlag;
            CanGenerateTrue = oldInfo.CanGenerateTrue;
            _currentEnumValues.Clear();
            _currentEnumValues.AddRange(oldInfo.Select(e => new EnumValueMeta(e.EnumName, e.IntValue, e.Attribute)));
        }

        public string NameType { get; }
        public bool IsFlag { get; }
        public bool CanGenerateTrue { get; }
        public int Count => _currentEnumValues.Count;

        public IEnumValueMeta GetEnumMeta(int i)
        {
            return _currentEnumValues[i];
        }

        public IEnumerator<IEnumValueMeta> GetEnumerator()
        {
            var list = _currentEnumValues.ToList();
            list.Sort((o1, o2) => o1.IntValue.CompareTo(o2.IntValue));
            foreach (var t in list)
                yield return t;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        public void AddCandidate(string valueCandidate, string comment, string attributeValue, int? intValue)
        {
            var value = -1;
            // либо используем переданное значение, либо вычисляем сами следующие значение
            if (!intValue.HasValue || _currentEnumValues.Any(o => o.IntValue == intValue.Value))
            {
                value = GetNextIntValue();
            }
            else
            {
                value = intValue.Value;
            }

            if (!string.IsNullOrEmpty(comment))
            {
                attributeValue += "\t\t[UnityEngine.TooltipAttribute(\"" + comment + "\")]";
            }

            _currentEnumValues.Add(new EnumValueMeta(valueCandidate, value, attributeValue));
        }

        private int GetNextIntValue()
        {
            var lastIntValue = _currentEnumValues.Count == 0 ? 0 : _currentEnumValues.Last().IntValue;
            if (IsFlag)
            {
                var power = Convert.ToInt32(Math.Log(lastIntValue) / Math.Log(2));
                return (int) Mathf.Pow(2, power + 1);
            }

            return lastIntValue + 1;
        }

        public bool CheckCandidate(string valueCandidate)
        {
            return _currentEnumValues.FindIndex(o => o.EnumName == valueCandidate) == -1;
        }

        /// <summary>
        ///     Удалить по индексу
        /// </summary>
        /// <param name="i"></param>
        public void RemoveAt(int i)
        {
            _currentEnumValues.RemoveAt(i);
        }

        /// <summary>
        ///     Удалить значение по имени
        /// </summary>
        /// <param name="name"></param>
        public void RemoveWithName(string name)
        {
            _currentEnumValues.RemoveAll(o => o.EnumName == name);
        }
    }
}