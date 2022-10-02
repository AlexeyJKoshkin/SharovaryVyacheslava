using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Core.Parser
{
    public static class CommonTypeParser
    {
        public const char SPLIT_ARRAY_SYMBOL = ',';

        private static readonly CultureInfo CultureInfoFloatParse;

        static CommonTypeParser()
        {
            CultureInfoFloatParse                                       = (CultureInfo) CultureInfo.CurrentCulture.Clone();
            CultureInfoFloatParse.NumberFormat.CurrencyDecimalSeparator = "."; // устанавливаем символ разделителя десятичной дроби 
        }

        public static bool ParseBool(string value)
        {
            if (value.Length == 1)
            {
                switch (value[0])
                {
                    case '1': return true;
                    case '0': return false;
                }
            }

            return bool.TryParse(value, out var result) && result;
        }

        public static float ParseFloat(string value)
        {
            return float.TryParse(value, NumberStyles.Any, CultureInfoFloatParse, out var result) ? result : 0;
        }

        public static int ParseInt(string value)
        {
            return int.TryParse(value, out var result) ? result : 0;
        }

        public static string ParseString(string value)
        {
            if (value.StartsWith(" "))
            {
                value = value.Remove(0, 1);
            }

            if (value.EndsWith(" "))
            {
                value = value.Remove(value.Length - 1, 1);
            }

            return value;
        }

        public static Enum ParseEnum(Type enumType, string cellValue)
        {
            return (Enum) Enum.Parse(enumType, cellValue, true);
        }

        public static T ParseEnum<T>(string cellValue) where T : struct
        {
            return Enum.TryParse<T>(cellValue, true, out var result) ? result : default;
        }

        public static Array ParseEnumArray(Type arrayType, string cellValue)
        {
            Type     enumType = arrayType.GetElementType();
            string[] values   = cellValue.Split(SPLIT_ARRAY_SYMBOL).GetNonEmptyValues().ToArray();
            Array    array    = (Array) Activator.CreateInstance(arrayType, values.Length);
            for (int i = 0; i < values.Length; i++)
            {
                var value = ParseEnum(enumType, values[i]);
                array.SetValue(value, i);
            }

            return array;
        }

        public static object ParseArray(Type mainfieldType, string cellValue,
                                        Func<string, object> fieldParser,
                                        char splitChar = SPLIT_ARRAY_SYMBOL)
        {
            object[] values = cellValue.Split(splitChar).GetNonEmptyValues().Select(fieldParser.Invoke).ToArray();
            return FillArray(mainfieldType, values);
        }

        private static object FillArray(Type mainfieldType, object[] values)
        {
            Array array = (Array) Activator.CreateInstance(mainfieldType, values.Length);
            for (int i = 0; i < values.Length; i++)
                array.SetValue(values[i], i);

            return array;
        }

        private static IEnumerable<string> GetNonEmptyValues(this string[] values)
        {
            foreach (var value in values)
            {
                if (string.IsNullOrEmpty(value))
                {
                    continue;
                }

                yield return value;
            }
        }
    }
}